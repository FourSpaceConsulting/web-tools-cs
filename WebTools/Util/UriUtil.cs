using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourspace.WebTools.Util
{
    public static class UriUtil
    {
        private const char UriPathSeparator = '/';
        private static readonly Uri LOCALHOST = new Uri("http://localhost");

        /// <summary>
        /// Binds parameters to standard URL pattern
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string BindParameters(string uri, IDictionary<string, string> parameters)
        {
            if (parameters == null) return uri;
            UriTemplate template = new UriTemplate(uri);
            Uri namedUri = template.BindByName(LOCALHOST, parameters);
            return namedUri.PathAndQuery;
        }

        /// <summary>
        /// Appends a uri path separator to start of string if not present
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToAbsolutePath(string path)
        {
            if (path == null) return null;
            if (path.Length > 0 && path[0] == UriPathSeparator)
                return path;
            return UriPathSeparator + path;
        }


    }
}
