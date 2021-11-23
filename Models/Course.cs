﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicatieMeditatii.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Title is mandatory!")]
        public string Title { get; set; }


        public string SubjectId { get; set; }

        public virtual ICollection<CourseContent> CourseContents { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }
    }
}