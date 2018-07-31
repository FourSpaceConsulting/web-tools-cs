using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourspace.WebTools.Http
{
    /// <summary>
    /// Client for resource management using Http verbs
    /// </summary>
    public interface IApiClient : IDisposable
    {
        Task<T> Get<T>(string uri, IDictionary<string, string> parameters);
        Task<R> Post<T, R>(string uri, T postData, IDictionary<string, string> parameters);
        Task<bool> Delete(string uri, IDictionary<string, string> parameters);
    }
}
