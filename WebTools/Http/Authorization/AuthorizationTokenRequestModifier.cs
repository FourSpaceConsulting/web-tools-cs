using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fourspace.WebTools.Http.Authorization
{
    /// <summary>
    /// Adds an authorization token to the request
    /// </summary>
    public class AuthorizationTokenRequestModifier : IHttpRequestModifier
    {
        private const string AUTHORIZATION_TOKEN_NAME = "Authorization";
        private readonly IAuthorizationTokenProvider authorizationTokenProvider;

        public AuthorizationTokenRequestModifier(IAuthorizationTokenProvider authorizationTokenProvider)
        {
            this.authorizationTokenProvider = authorizationTokenProvider;
        }

        public void ModifyRequest(HttpRequestMessage request, string uri, IDictionary<string, string> parameters)
        {
            request.Headers.Add(AUTHORIZATION_TOKEN_NAME, authorizationTokenProvider.GetAuthorizationToken());
        }
    }
}
