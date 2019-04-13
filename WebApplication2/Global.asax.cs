using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebApplication2.Principal.Model;

namespace WebApplication2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                UserPrincipalSerializeModel serializeModel = serializer.Deserialize<UserPrincipalSerializeModel>(authTicket.UserData);

                UserPrincipal newUser = new UserPrincipal(authTicket.Name);
                newUser.Id = serializeModel.Id;
                newUser.FirstName = serializeModel.FirstName;
                newUser.LastName = serializeModel.LastName;

                HttpContext.Current.User = newUser;
            }
        }
    }
}
