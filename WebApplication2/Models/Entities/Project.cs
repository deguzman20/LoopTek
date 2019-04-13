using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Entities
{
    public class Project
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string YoutubeLink { get; set; }
        public string GooglePlayLink { get; set; }
        public string ImageLink { get; set; }
        public string PlatformID { get; set; }
        public string CategoryID { get; set; }
        public string SubCategoryID { get; set; }
        public string imagedisplaytype { get; set; }
        public bool HasIOS { get; set; }
        public bool HasAndroid { get; set; }

        public IEnumerable<Image> CarouselImages { get; set; }
    }
}