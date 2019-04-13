using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication2.Models.Entities;

namespace WebApplication2.Models
{
    public class LoopContext : DbContext
    {
        public LoopContext() : base("LoopDB") { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Person> Team { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<HomeCarousel> Carousel { get; set; }


    }
}