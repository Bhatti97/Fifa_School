using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fifa_school.Models
{
    public class Branch
    {
        [Key]
        public int Branch_id { get; set; }
        public string Branch_name { get; set; }
        public string Branch_Address { get; set; }
        public string Branch_Contact { get; set; }
    }
}