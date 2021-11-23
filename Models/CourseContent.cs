using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicatieMeditatii.Models
{
    public class CourseContent
    {
        [Key]
        public int Id { get; set; }

        public int Courseid { get; set; }

        public int Index { get; set; }
        // shows the order of the contents

        public string Type { get; set; }
        // 'text' or 'image'

        public string Text { get; set; }

        public string ImagePath { get; set; }

    }
}