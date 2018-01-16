using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fifa_school.Models
{
    public class Users
    {
        [Key]
        public int user_id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string user_role { get; set; } 
        public bool status { get; set; }
    }
}