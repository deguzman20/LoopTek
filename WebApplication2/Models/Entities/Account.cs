using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Entities
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HashPassword { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string EmailHashPassword { get; set; }
        public string Salt { get; set; }

    }
}