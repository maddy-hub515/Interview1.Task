using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interview1.Task.Models
{
    public class Student
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Mark1 { get; set; }
        [Required]
        public int Mark2 { get; set; }
    }
}