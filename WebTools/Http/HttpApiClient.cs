using Fourspace.WebTools.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Fourspace.WebTools.Http
{

    /// <summary>
    /// HTTP client implementation
    /// </summary>
    /// <typeparam name="C">modifier context</typeparam>
    public class HttpApiClient : IApiClient, IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HttpClient client;
        private readonly IHttpRequestModifier modifier;
        private readonly MediaTypeFormatter mediaTypeFormatter;
        private bool disposed;

        public HttpApiClient(string baseUri, string mediaType, MediaTypeFormatter mediaTypeFormatter)
        {
            this.mediaTypeFormatter = mediaTypeFormatter;
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }

        public HttpApiClient(string baseUri, string mediaType, MediaTypeFormatter mediaTypeFormatter, IHttpRequestModifier modifier) : this(baseUri, mediaType, mediaTypeFormatter)
        {
            this.modifier = modifier;
        }

        public Task<T> Get<T>(string uri, IDictionary<string, string> parameters)
        {
            return Send<object, T>(HttpMethod.Get, uri, null, parameters);
        }

        public Task<R> Post<T, R>(string uri, T postData, IDictionary<string, string> parameters = null)
        {
            return Send<T, R>(HttpMethod.Post, uri, postData, parameters);
        }

        public Task<bool> Delete(string uri, IDictionary<string, string> parameters = null)
        {
            return Send<object, bool>(HttpMethod.Delete, uri, null, parameters);
        }

        private async Task<R> Send<T, R>(HttpMethod method, string uri, T postData, IDictionary<string, string> parameters)
        {
            HttpResponseMessage response = null;
            HttpContent content = null;
            try
            {
                // Create content if needed
                content = CreateContent(method, postData);
                // bind parameters and send
                string parameterUri = UriUtil.BindParameters(uri, parameters);
                if (modifier != null)
                {
                    var request = new HttpRequestMessage(method, parameterUri);
                    request.Content = content;
                    modifier.ModifyRequest(request, uri, parameters);
                    response = await client.SendAsync(request).ConfigureAwait(false);
                }
                else
                {
                    response = await SendAsync(method, parameterUri, content).ConfigureAwait(false);
                }
                if (!response.IsSuccessStatusCode) throw new HttpResponseException(response);
                return await response.Content.ReadAsAsync<R>();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                throw;
            }
        }

        private HttpContent CreateContent<T>(HttpMethod method, T postData)
        {
            HttpContent content = null;
            if (postData != null &&
                (HttpMethod.Post.Equals(method) || HttpMethod.Put.Equals(method)))
            {
                content = new ObjectContent<T>(postData, mediaTypeFormatter);
            }
            return content;
        }

        private Task<HttpResponseMessage> SendAsync(HttpMethod method, string parameterUri, HttpContent content)
        {
            return HttpMethod.Get.Equals(method) ?
                    client.GetAsync(parameterUri) :
                    client.PostAsync(parameterUri, content);
        }

        #region disposal
        /// <summary>
        /// This disposes the object. 
        /// It cannot be used again after this call.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    if (client != null)
                    {
                        client.Dispose();
                    }
                }
                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
