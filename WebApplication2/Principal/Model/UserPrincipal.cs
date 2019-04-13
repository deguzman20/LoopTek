using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebApplication2.Principal.Interfaces;

namespace WebApplication2.Principal.Model
{
    public class UserPrincipal : IUserPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }

        public UserPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }
        public string Role { get; set; }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}