using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.Entities;

namespace WebApplication2.Models.DTO
{
    public class TeamViewModel
    {
        public Person per { get; set; }
        public HttpPostedFileBase FileMain { get; set; }
    }
}