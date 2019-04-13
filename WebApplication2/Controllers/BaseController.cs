using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Principal.Model;

namespace WebApplication2.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected virtual new UserPrincipal User
        {
            get { return HttpContext.User as UserPrincipal; }
        }
    }
}