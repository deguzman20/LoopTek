using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Entities;

namespace WebApplication2.Models.DTO
{
    public class ProjectViewModel
    {
        public ProjectViewModel() {
        }
        public Project project { get; set; }
        public IEnumerable<SelectListItem> projectservices { get; set; }
        public HttpPostedFileBase FileMain { get; set; }
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
        public IEnumerable<Image> images { get; set; }

    }
}