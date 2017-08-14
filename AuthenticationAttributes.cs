using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace InventoryApi
{
    public class AuthenticationAttributes : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }

            string authenticationtoken = actionContext.Request.Headers.Authorization.Parameter;
            string decodedauthentication = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationtoken));
            string[] logincredentials = decodedauthentication.Split(':');
            string username = logincredentials[0];
            string password = logincredentials[1];

            if (LoginSecurity.login(username, password))
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
            }
            else
            {
                actionContext.Response = actionContext.Request
                .CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}