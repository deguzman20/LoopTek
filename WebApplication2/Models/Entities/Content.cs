using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Entities
{
    public class Content
    {
        [Key]
        public int ID { get; set; }
        public string Mission { get; set; }
        public string Vision { get; set; }
        public string AboutUs { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string InvestWUs { get; set; }
        public string WorkWUs { get; set; }
        public string headercolor { get; set; }
        public string loopcolor { get; set; }
        public string tekcolor { get; set; }
        public string bannerimage { get; set; }
        public string aboutImage { get; set; }
        public string InvestWUsBanner { get; set; }
        public string WorkWUsBanner { get; set; }
        public string Email { get; set; }


    }
}