using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fourspace.WebTools.Http
{
    /// <summary>
    /// Exception for Http API calls.
    /// </summary>
    public class HttpResponseException : SystemException
    {
        /// <summary>
        /// Validation result.
        /// </summary>
        public HttpResponseMessage Response { get; }

        /// <summary>
        /// Construct with validation result.
        /// </summary>
        /// <param name="response">Result</param>
        public HttpResponseException(HttpResponseMessage response) : base("Http Response Error") { Response = response; }
    }
}
