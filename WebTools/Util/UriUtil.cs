/*
MIT License

Copyright (c) 2017 Richard Steward

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections.Generic;

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
