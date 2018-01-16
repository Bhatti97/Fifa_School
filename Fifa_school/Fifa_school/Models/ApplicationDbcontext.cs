using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fifa_school.Models
{
    public class ApplicationDbcontext:DbContext
    {
        public ApplicationDbcontext():base("Default")
        {

        }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}