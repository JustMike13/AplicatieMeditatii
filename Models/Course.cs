using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicatieMeditatii.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Title is mandatory!")]
        public string Title { get; set; }

        public string Year { get; set; }

        public string SubjectId { get; set; }
    }
}