using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.DTO;
using WebApplication2.Models.Entities;

namespace WebApplication2.Controllers
{
    public class ProjectsController : Controller
    {
        LoopContext database = new LoopContext();
        // GET: Projects
        public ActionResult Index()
        {
            CategoryViewModel datas = new CategoryViewModel();
            Category cat = database.Categories.FirstOrDefault();

            return RedirectToAction("ViewApps" , (new { catID = cat.ID }));
        }
        public ActionResult ViewApps(int catID)
        {
            CategoryViewModel datas = new CategoryViewModel();
            Category cat = database.Categories.Where(m => m.ID.Equals(catID)).FirstOrDefault();
            IEnumerable<Project> data = database.Projects.Where(m => m.CategoryID == catID.ToString()).ToList();
            datas.project = data;
            datas.category = cat;
            return View(datas);
        }
        public ActionResult WebApps()
        {
            IEnumerable<Project> data = database.Projects.Where(m => m.CategoryID == "2").ToList();
            return View(data);
        }
        public ActionResult MobileApps()
        {
            IEnumerable<Project> data = database.Projects.Where(m => m.CategoryID == "3").ToList();
            return View(data);
        }
        public ActionResult DesktopApps()
        {
            IEnumerable<Project> data = database.Projects.Where(m => m.CategoryID == "4").ToList();
            return View(data);
        }

        public ActionResult GamePreview()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GamePreview(int gameid)
        {
            PreviewModel pro = new PreviewModel();
            Project data = database.Projects.Where(m => m.ID.Equals(gameid) ).FirstOrDefault();
            List<Image> images = database.Images.Where(p => p.ProjectId.Equals(gameid)).ToList();
            pro.proj = data;
            pro.images = images;
            return View(pro);
        }
        public ActionResult TopNav()
        {
            var nav = database.Categories.ToList();
            return PartialView("_NavPartial", nav);
        }
    }
}