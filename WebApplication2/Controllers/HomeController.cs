using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.DTO;
using WebApplication2.Models.Entities;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {

        LoopContext database = new LoopContext();
        public ActionResult Index()
        {
            HomeViewModel home = new HomeViewModel();
            Content data = database.Contents.Where(m => m.ID.Equals(1)).SingleOrDefault();
            home.content = data;
            home.services = database.Categories.ToList();
            home.images = database.Carousel.ToList();
            home.team = database.Team.ToList();
            return View(home);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public JsonResult SendEmail(string name,string email,string message)
        {
            Message msgs = new Message();
            msgs.SenderName = name;
            msgs.EmailFrom = email;
            msgs.Content = message;
            database.Messages.Add(msgs);
            database.SaveChanges();
            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            try
            {
                msg.Subject = "LoopTek";
                msg.Body = "Message from: " + name + " " + message;
                msg.From = new MailAddress("loorenzpascual45@gmail.com");
                msg.To.Add(email);
                msg.IsBodyHtml = true;
                client.Host = "mail.loopteksolutions.com";
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("lorenz45@loopteksolutions.com", "pascual123");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.Send(msg);
            }
            catch (Exception ex)
                {
            }
            //MailMessage mail = new MailMessage();
            //try
            //{
            //SmtpClient smtpClient = new SmtpClient();
            //smtpClient.UseDefaultCredentials = true;

            //mail.From = new MailAddress("lorenz@loopteksolutions.com");
            //mail.To.Add(new MailAddress(email));
            //mail.Subject = "";

            //mail.Body = "body";
            //mail.IsBodyHtml = false;

            //smtpClient.Send(mail);
            //}
            //catch (Exception ex)
            //{
            //}
            return Json("Success");
        }
    }
}