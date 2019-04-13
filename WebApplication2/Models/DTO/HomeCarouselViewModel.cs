using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.Entities;

namespace WebApplication2.Models.DTO
{
    public class HomeCarouselViewModel
    {
        public IEnumerable<HomeCarousel> images { get; set; }
    }
}