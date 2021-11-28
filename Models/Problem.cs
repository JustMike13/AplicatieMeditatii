using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicatieMeditatii.Models
{
    public class Problem
    {
        [Key]
        public int ProblemId { get; set; }

        [Required(ErrorMessage = "Title is mandatory!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Question is mandatory!")]
        public string Question { get; set; }

        [Required(ErrorMessage = "First two answers are mandatory!")]
        public string a { get; set; }

        [Required(ErrorMessage = "First two answers are mandatory!")]
        public string b { get; set; }
        public string c { get; set; }
        public string d { get; set; }
        public string Answer { get; set; }
        public string Correct { get; set; }

        public int SubjectId { get; set; }
        public IEnumerable<SelectListItem> Subjects { get; set; }
    }
}