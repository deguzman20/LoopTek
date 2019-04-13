using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Entities
{
    public class Person
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageLocation { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }

    }
}