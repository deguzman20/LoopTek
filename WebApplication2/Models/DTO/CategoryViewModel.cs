using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.Entities;

namespace WebApplication2.Models.DTO
{
    public class CategoryViewModel
    {
        public Category category { get; set; }
        public IEnumerable<Project> project { get; set; }
    }
}