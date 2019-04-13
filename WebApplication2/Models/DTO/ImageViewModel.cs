using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.Entities;
namespace WebApplication2.Models.DTO
{
    public class ImageViewModel
    {
        public Project proj { get; set; }
        public IEnumerable<Image> images { get; set; }
    }
}