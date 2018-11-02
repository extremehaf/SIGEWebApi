using System.Web.Http.Controllers;
using SIGEWebApi.Security;

namespace SIGEWebApi.Filters
{
    public class WindowsBasicAuthenticationFilter : BasicAuthenticationFilter
    {

        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            var ret = (Win32Logon.Login(username, password) == LoginStatus.Success);
            return ret;
        }
    }
}