using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class TestCMS
    {

        [Display(Name = "Enter Your Blog Title Here")]
        [Required(ErrorMessage = "Please Enter Title")]
        public string Title { get; set; }

        [Display(Name = "Set Date")]
        [Required(ErrorMessage = "Please Set The Date")]
        public DateTime Date { get; set; }

       
        public string Content { get; set; }
    }
}