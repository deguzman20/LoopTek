using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Entities;
using WebApplication2.Common;
using WebApplication2.Models;
using WebApplication2.Principal.Model;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace WebApplication2.Controllers
{
    public class AccountController : BaseController
    {

        LoopContext database = new LoopContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string email,string pass)
        {
            var user = database.Accounts.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                string hashstring = Cryptography.HashPassword(pass);
                string HashPassword = Cryptography.HashString(hashstring, user.Salt);
                if (user.HashPassword == HashPassword)
                {
                    UserPrincipalSerializeModel serializeModel = new UserPrincipalSerializeModel();
                    serializeModel.Id = user.ID;
                    serializeModel.FirstName = user.FirstName;
                    serializeModel.LastName = user.LastName;

                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    string userData = serializer.Serialize(serializeModel);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                             1,
                             email,
                             DateTime.Now,
                             DateTime.Now.AddMinutes(15),
                             false,
                             userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    return RedirectToAction("Index", "admin");

                }
                else
                {
                    return RedirectToAction("Index", "account");

                }

            }
            else {
                return RedirectToAction("Index", "account");
            }
        }

        public ActionResult Register()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(string email,string fname,string lname,string pass)
        {
            Account User = new Account();
            string salt = Cryptography.CreateSalt();
            string hashstring = Cryptography.HashPassword(pass);
            User.Salt = salt;
            User.HashPassword = Cryptography.HashString(hashstring , salt);
            User.Email = email;
            User.FirstName = fname;
            User.LastName = lname;
            database.Accounts.Add(User);
            database.SaveChanges();
            return RedirectToAction("Index","Admin");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}