using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fourspace.WebTools.Http
{
    /// <summary>
    /// Modify an http request (eg. modify header info).
    /// </summary>
    public interface IHttpRequestModifier
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        void ModifyRequest(HttpRequestMessage request, string uri, IDictionary<string, string> parameters);
    } 
}
