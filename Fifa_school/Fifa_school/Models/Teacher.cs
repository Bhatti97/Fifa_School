using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fifa_school.Models
{
    public class Teacher
    {
        [Key]
        public int Teacher_id { get; set; }
        public string Teacher_Name { get; set; }
        public string Teacher_FatherName { get; set; }
        public DateTime Dateofjoin { get; set; }

        public int Branch_id { get; set; }
        public virtual Branch Branch { get; set; }
    }
}