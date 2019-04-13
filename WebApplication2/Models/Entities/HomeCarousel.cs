using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Entities
{
    public class HomeCarousel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}