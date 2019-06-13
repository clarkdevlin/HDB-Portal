using HDBLibrary;
using HDBLibrary.Models;
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

namespace WebAPI
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

                var decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                var usernamePasswordArray = decodedAuthToken.Split(':');

                var username = usernamePasswordArray[0];
                var password = usernamePasswordArray[1];
                
                if (UserModel.Login(username, password))
                {
                    var userId = DBHelper.ExecuteSQL<string>(@"select Id
                                                             from dbo.ASPNETUSERS
                                                             where UserName = @UserName", new { UserName = username });

                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userId), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}