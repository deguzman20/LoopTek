using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication2.Common;
using WebApplication2.Models;
using WebApplication2.Models.DTO;
using WebApplication2.Models.Entities;
using WebApplication2.Principal.Model;

namespace WebApplication2.Controllers
{
    [LoopAuthorize]
    public class AdminController : BaseController
    {
        LoopContext database = new LoopContext();
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("editcontent","admin");
        }
        [HttpPost]
        public ActionResult Index(Account acc)
        {
            var account = database.Accounts.Where(a => a.FirstName.Equals(acc.FirstName)&&a.HashPassword.Equals(acc.HashPassword)).FirstOrDefault();
            return RedirectToAction("admin","projectlist");
        }
        ////services
        public ActionResult ServicesList()
        {
            var dat = database.Categories.ToList();
            return View(dat);
        }
        public ActionResult Service()
        {
            return View();
        }
        public ActionResult AddService()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddService(Category service,HttpPostedFileBase BannerLocation,HttpPostedFileBase IconLocation)
        {
            if (BannerLocation != null)
            {
                var fileName = Path.GetFileName(BannerLocation.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName);
                service.BannerLocation = "/Images/" + fileName;
                BannerLocation.SaveAs(path);
            }
            if (IconLocation != null)
            {
                var fileName = Path.GetFileName(IconLocation.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName);
                service.IconLink = "/Images/" + fileName;
                IconLocation.SaveAs(path);
            }
            database.Categories.Add(service);
            database.SaveChanges();
            if (database.SaveChanges()<0)
            {
                ViewBag.Message = "Success";
            }
            else
            {
                ViewBag.Message = "Failed";
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditService(int xid)
        {
            Category te = database.Categories.SingleOrDefault(tea => tea.ID == xid);
            return View(te);
        }
        [HttpPost]
        public ActionResult EditService(Category service, HttpPostedFileBase BannerLocation,HttpPostedFileBase IconLocation)
        {
            Category te = database.Categories.SingleOrDefault(tea => tea.ID == service.ID);
            te.Name = service.Name;
            te.Description = service.Description;
            if (BannerLocation != null)
            {
                var fileName = Path.GetFileName(BannerLocation.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName);
                te.BannerLocation = "/Images/" + fileName;
                BannerLocation.SaveAs(path);
            }

            if (IconLocation != null)
            {
                var fileName = Path.GetFileName(IconLocation.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName);
                service.IconLink = "/Images/" + fileName;
                IconLocation.SaveAs(path);
            }
            database.SaveChanges();
            return RedirectToAction("Serviceslist","admin");

        }
        [HttpGet]
        public ActionResult DeleteService(int xid)
        {
            Category te = database.Categories.SingleOrDefault(tea => tea.ID == xid);
            database.Categories.Remove(te);
            database.SaveChanges();
            return RedirectToAction("Serviceslist", "admin");
        }
        public ActionResult PlatformList()
        {
            var dat = database.Platforms.ToList();
            return View(dat);
        }
        public ActionResult AddPlatform()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPlatform(Platform platform)
        {

            database.Platforms.Add(platform);
            database.SaveChanges();
            if (database.SaveChanges() < 0)
            {
                ViewBag.Message = "Success";
            }
            else
            {
                ViewBag.Message = "Failed";
            }
            return View();
        }

        public ActionResult EditPlatform()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditPlatform(Platform platform)
        {
            Platform te = database.Platforms.SingleOrDefault(tea => tea.ID == platform.ID);
            te.Name = platform.Name;
            te.Description = platform.Description;

            database.SaveChanges();
            return View();
        }
        public ActionResult DeletePlatform()
        {
            return View();
        }
        public ActionResult ProjectList()
        {
            List<ProjectViewModel> projects = new List<ProjectViewModel>();
            var dat = database.Projects.ToList();
            foreach (var project in dat) {
                ProjectViewModel pro = new ProjectViewModel();
                List<Image> images = database.Images.Where(i => i.ProjectId.Equals(project.ID)).ToList();
                pro.images = images;
                pro.project = project;
                projects.Add(pro);
            }
            return View(projects);
        }
        public ActionResult AddProject()
        {
            ProjectViewModel model = new ProjectViewModel
            {
                projectservices = GetServices()
            };
            return View(model);
        }
        private IEnumerable<SelectListItem> GetServices()
        {
            var service = database.Categories
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(service, "Value", "Text");
        }

        [HttpPost]
        public ActionResult AddProject(ProjectViewModel projectView)
        {
            List<Image> imgs = new List<Image>();
            if (projectView.FileMain != null)
            {
                    var fileName = Path.GetFileName(projectView.FileMain.FileName);
                    var path = Path.Combine(Server.MapPath("/Images"), fileName);
                projectView.project.ImageLink = "/Images/"+fileName;
                projectView.FileMain.SaveAs(path);
            }
            if (projectView.Files != null)
            {
                foreach (HttpPostedFileBase file in projectView.Files)
                {
                    Image img = new Image();
                    var InputFileName = Path.GetFileName(file.FileName);
                    var ServerSavePath = Path.Combine(Server.MapPath("/Images"), InputFileName);
                    //Save file to server folder  
                    img.Name = InputFileName;
                    img.Path = "/Images/" + InputFileName;
                    file.SaveAs(ServerSavePath);
                    imgs.Add(img);
                    
                }

            }
            projectView.project.CarouselImages = imgs;
            database.Projects.Add(projectView.project);
            database.SaveChanges();
            if (database.SaveChanges() < 0)
            {
                ViewBag.Message = "Success";
            }
            else
            {
                ViewBag.Message = "Failed";
            }
            return RedirectToAction("addProject","admin");
        }
        [HttpGet]
        public ActionResult EditProject(int xid)
        {
            ProjectViewModel pro = new ProjectViewModel();
            Project project = database.Projects.Where(p => p.ID.Equals(xid)).FirstOrDefault();
            IEnumerable<Image> img = database.Images.Where(p => p.ProjectId.Equals(xid)).ToList();
            pro.images = img;
            pro.project = project;
            pro.projectservices = GetServices();
            return View(pro);
        }
        public ActionResult EditProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditProject(ProjectViewModel projectView)
            {
            
            Project te = database.Projects.SingleOrDefault(tea => tea.ID == projectView.project.ID);
            te.Name = projectView.project.Name;
            te.Description = projectView.project.Description;
            te.CategoryID = projectView.project.CategoryID;
            te.GooglePlayLink = projectView.project.GooglePlayLink;
            te.HasAndroid = projectView.project.HasAndroid;
            te.YoutubeLink = projectView.project.YoutubeLink;
            //te.CarouselImages = projectView.project.CarouselImages;

            if (projectView.FileMain != null)
            {
                var fileName = Path.GetFileName(projectView.FileMain.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName);
                te.ImageLink = "/Images/" + fileName;
                projectView.FileMain.SaveAs(path);
            }
            database.SaveChanges();
            return RedirectToAction("projectlist","admin");
        }
        [HttpGet]
        public ActionResult DeleteProject(int xid)
        {
            Project project = database.Projects.Where(p => p.ID.Equals(xid)).FirstOrDefault();
                database.Projects.Remove(project);
            database.SaveChanges();
            return RedirectToAction("projectlist", "admin");
        }

        public JsonResult ShowData(string stud)
        {
            return Json(stud, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditContent()
        {
            Content te = database.Contents.SingleOrDefault(x =>x.ID.Equals(1));
            return View(te);
        }
        [HttpPost]
        public ActionResult EditContent(Content content,HttpPostedFileBase AboutPage, HttpPostedFileBase Work, HttpPostedFileBase Invest)
        {
            Content te = database.Contents.SingleOrDefault(tea => tea.ID == content.ID);
            te.AboutUs = content.AboutUs;
            te.Address = content.Address;
            te.Email = content.Email;
            te.ContactNumber = content.ContactNumber;
            te.headercolor = content.headercolor;
            te.InvestWUs = content.InvestWUs;
            te.loopcolor = content.loopcolor;
            te.Mission = content.Mission;
            te.tekcolor = content.tekcolor;
            te.Vision = content.Vision;
            te.WorkWUs = content.WorkWUs;

            if (AboutPage != null)
            {
                var fileName = Path.GetFileName(AboutPage.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName.Trim().Replace(" ", ""));
                te.aboutImage = "/Images/" + fileName.Trim().Replace(" ", "");
                AboutPage.SaveAs(path);
            }
            if (Invest != null)
            {
                var fileName = Path.GetFileName(Invest.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName.Trim().Replace(" ",""));
                te.InvestWUsBanner = "/Images/" + fileName.Trim().Replace(" ", "");
                Invest.SaveAs(path);
            }
            if (Work != null)
            {
                var fileName = Path.GetFileName(Work.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName.Trim().Replace(" ", ""));
                te.WorkWUsBanner = "/Images/" + fileName.Trim().Replace(" ", "");
                Work.SaveAs(path);
            }
            database.SaveChanges();
            return RedirectToAction("EditContent","admin");
        }
        public ActionResult Team()
        {
            var dat = database.Team.ToList();
            return View(dat);
        }
        public ActionResult AddTeam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTeam(TeamViewModel team)
        {
            if (team.FileMain != null)
            {
                var fileName = Path.GetFileName(team.FileMain.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName);
                team.per.ImageLocation = "/Images/" + fileName;
                team.FileMain.SaveAs(path);
            }

            database.Team.Add(team.per);
            database.SaveChanges();
            if (database.SaveChanges() < 0)
            {
                ViewBag.Message = "Success";
            }
            else
            {
                ViewBag.Message = "Failed";
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditTeam(int xid)
        {
            TeamViewModel team = new TeamViewModel();
            Person te = database.Team.SingleOrDefault(tea => tea.ID == xid);
            team.per = te;
            return View(team);
        }
        [HttpPost]
        public ActionResult EditTeam(TeamViewModel team)
        {
            Person te = database.Team.SingleOrDefault(tea => tea.ID == team.per.ID);
            te.Name = team.per.Name;
            te.Position = team.per.Position;
            te.Email = team.per.Email;
            if (team.FileMain != null)
            {
                var fileName = Path.GetFileName(team.FileMain.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName);
                te.ImageLocation = "/Images/" + fileName;
                team.FileMain.SaveAs(path);
            }
            database.SaveChanges();
            return RedirectToAction("team","admin");
        }
        [HttpGet]
        public ActionResult DeleteTeam(int xid)
        {
            Person per = database.Team.Where(p => p.ID.Equals(xid)).FirstOrDefault();
            database.Team.Remove(per);
            database.SaveChanges();
            return RedirectToAction("team","admin");
        }
        [HttpGet]
        public ActionResult CarouselImages(int xid)
        {
            ImageViewModel pro = new ImageViewModel();
            IEnumerable<Image> images = database.Images.Where(p => p.ProjectId.Equals(xid)).ToList();
            Project project = database.Projects.Where(p => p.ID.Equals(xid)).FirstOrDefault();
            pro.images = images;
            pro.proj = project;
            return View(pro);
        }
        [HttpPost]
        public ActionResult AddImage(int projectID,HttpPostedFileBase FileMain)
        {
            Image img = new Image();
            if (FileMain != null)
            {
                var fileName = Path.GetFileName(FileMain.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName);
                Project project = database.Projects.Where(p => p.ID.Equals(projectID)).FirstOrDefault();
                img.Path = "/Images/" + fileName;
                FileMain.SaveAs(path);
                img.Name = fileName;
                img.Project = project;
                database.Images.Add(img);
                database.SaveChanges();
                if (database.SaveChanges() < 0)
                {
                    ViewBag.Message = "Success";
                }
                else
                {
                    ViewBag.Message = "Failed";
                }
            }
            return RedirectToAction("CarouselImages",(new { xid = projectID }));
         }
        [HttpGet]
        public ActionResult DeleteImage(int xid)
        {
            Image img = database.Images.Where(p => p.ID.Equals(xid)).FirstOrDefault();
            database.Images.Remove(img);
            database.SaveChanges();
            if (img.ProjectId.Equals(7)) {
                return RedirectToAction("HomeCarousel");
            }
            else { 
            return RedirectToAction("CarouselImages", (new { xid = img.ProjectId }));
            }
        }

        [HttpGet]
        public ActionResult DeleteHomeImage(int xid)
        {
            HomeCarousel img = database.Carousel.Where(p => p.ID.Equals(xid)).FirstOrDefault();
            database.Carousel.Remove(img);
            database.SaveChanges();
                return RedirectToAction("HomeCarousel");
        }
        public ActionResult Register() {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Account acc)
        {
            database.Accounts.Add(acc);
            return RedirectToAction("admin","accountlist");
        }

        public ActionResult AccountList()
        {
            var dat = database.Accounts.ToList();
            return View(dat);
        }

    [HttpGet]
    public ActionResult HomeCarousel()
    {
        List<HomeCarousel> images = database.Carousel.ToList();
        return View(images);
    }

        public ActionResult AddCarImage(HttpPostedFileBase FileMain)
        {
            HomeCarousel img = new HomeCarousel();
            if (FileMain != null)
            {
                //Project proj = new Project();
                //proj.ID = 1;
                var fileName = Path.GetFileName(FileMain.FileName);
                var path = Path.Combine(Server.MapPath("/Images"), fileName);
                img.Path = "/Images/" + fileName;
                FileMain.SaveAs(path);
                img.Name = fileName;
                database.Carousel.Add(img);
                database.SaveChanges();
                if (database.SaveChanges() < 0)
                {
                    ViewBag.Message = "Success";
                }
                else
                {
                    ViewBag.Message = "Failed";
                }
            }
                return RedirectToAction("HomeCarousel");
        }
        public ActionResult MessagesList()
        {
            var dat = database.Messages.ToList();
            return View(dat);
        }
        public ActionResult EmailConfig()
        {
            //var dat = database.Messages.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult EmailConfig(Account acc)
        {
            var u = System.Web.HttpContext.Current.User as UserPrincipal;

            Account User = database.Accounts.Where(p => p.ID.Equals(u.Id)).FirstOrDefault();
            string hashstring = Cryptography.HashPassword(acc.EmailHashPassword);
            User.EmailHashPassword = hashstring;
            //database.Accounts.Add(User);
            database.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
    }

}