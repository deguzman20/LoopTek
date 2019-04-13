using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.Entities;

namespace WebApplication2.Models.DTO
{
    public class HomeViewModel
    {
        public Content content { get; set; }
        public List<HomeCarousel> images { get; set; }
        public IEnumerable<Category> services { get; set; }
        public IEnumerable<Person> team { get; set; }
    }
}