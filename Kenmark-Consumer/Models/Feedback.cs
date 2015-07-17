using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class Feedback
    {
        [Display(Name = "Rate The Color Choices")]
        [Required(ErrorMessage = "Please Rate the Color")]
        public int Color { get; set; }

        [Display(Name = "Rate The Design")]
        [Required(ErrorMessage = "Please Rate the Design")]
        public int Design { get; set; }

        public string Comments { get; set; }
        public string Email { get; set; }
        public string Style {get;set;}

        public void SaveFeedback(Feedback f)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                Frame_Feedback fb = new Frame_Feedback()
                {
                    color_rating = f.Color,
                    shape_rating = f.Design,
                    comment = f.Comments,
                    date = DateTime.Now,
                    email = f.Email,
                    style = f.Style
                };
                db.Frame_Feedback.Add(fb);
                db.SaveChanges();                         
            }
        }
    }
}