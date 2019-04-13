using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Principal.Model
{
    public class LoopAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _role;
        public LoopAuthorizeAttribute(string role = "")
        {
            _role = role;
        }

        protected virtual UserPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as UserPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return CurrentUser == null ? false : true;
        }

        private static bool SkipAuthorization(AuthorizationContext filterContext)
        {
            Contract.Assert(filterContext != null);

            return filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                   || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any();
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (this.AuthorizeCore(filterContext.HttpContext) || SkipAuthorization(filterContext))
            {
                if (String.IsNullOrEmpty(_role) || CurrentUser.Role.ToUpper().Equals(_role.ToUpper()))
                {
                    base.OnAuthorization(filterContext);

                }
                else
                {
                    RedirectToRouteResult routeData = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                           new
                           {
                               controller = "home",
                               action = "index",
                           }));
                    filterContext.Result = routeData;
                }

            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;

            if (CurrentUser == null)
            {
                routeData = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        controller = "home",
                        action = "index",
                    }));
            }

            filterContext.Result = routeData;
        }
    }
}