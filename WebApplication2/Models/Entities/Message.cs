using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Entities
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public string Content { get; set; }
        public string SenderName { get; set; }
        public string About { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
    }
}