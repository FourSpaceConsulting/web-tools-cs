using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourspace.WebTools.Http.Authorization
{
    public interface IAuthorizationTokenProvider
    {
        string GetAuthorizationToken();
    }
}
