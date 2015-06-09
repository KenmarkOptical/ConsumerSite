using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class Contact
    {
        [Display(Name = "First*")]
        [Required(ErrorMessage = "A First Name is required")]
        public string first { get; set; }

        [Display(Name = "Last*")]
        [Required(ErrorMessage = "A Last Name is required")]
        public string last { get; set; }

        [Display(Name = "Phone Number*")]
        [Required(ErrorMessage = "A Phone Number is required")]
        public string phone { get; set; }

        [Display(Name = "Email Address*")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "State")]
        public string state { get; set; }

        [Display(Name = "Zip Code")]
        public string zip { get; set; }

        [Display(Name = "Street Address")]
        public string address { get; set; }

        [Display(Name = "Comments/Questions*")]
        [Required(ErrorMessage = "A Question or Comment is required")]
        public string comment { get; set; }

        public bool SaveContact(Contact c)
        {
            //store to the database
            using(KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var cu = new contact_us()
                {
                    address = c.address,
                    city = c.city,
                    comment = c.comment,
                    email = c.email,
                    first = c.first,
                    last = c.last,
                    phone = c.phone,
                    state = c.state,
                    zip = c.zip
                };
                        
                db.contact_us.Add(cu);
                db.SaveChanges();
            }

            //send the email
            string to = Common.Environment == "live" ? "info@kenmarkoptical.com" : "mattcarden@kenmarkoptical.com";

            //create the email body
            string html =
               "<table> " +
               "<tr><td style='font-weight:bold;'>Name: </td>" +
               "<td>" + c.first + " " + c.last + "<td></tr>" +

               "<tr><td style='font-weight:bold; background-color:#DDD;'>Phone: </td>" +
               "<td style='background-color:#DDD;'>" + c.phone + "<td></tr>" +

               "<tr><td style='font-weight:bold;' valign='top'>Address: </td>" +
               "<td>" + c.address + "," + c.city + ", " + c.state + ", " + c.zip + "<td></tr>" +

               "<tr><td style='font-weight:bold; background-color:#DDD;'>Comments/Question: </td>" +
               "<td style='background-color:#DDD;'>" + c.comment + "<td></tr></table>";

            //send the email
            Email.SendEmail(c.email, new List<string>() { to }, new List<string>(), "Contact Request", html);

            //return success
            return true;
        }
    }
}