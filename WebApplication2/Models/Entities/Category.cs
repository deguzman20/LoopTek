using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Entities
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Isgame { get; set; }
        public string BannerLocation { get; set; }
        public string IconLink { get; set; }

        public Category Subcategory { get; set; }

    }
}