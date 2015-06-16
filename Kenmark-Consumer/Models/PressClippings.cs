using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Models
{
    public class PressClipping
    {
        public int Page { get; set; }
        public int Filter_Like_Collection { get; set; }
        public DateTime Filter_DateRange { get; set; }
        public List<Press_Clippings> Items { get; set; }

        public static List<SelectListItem> Date_List = new List<SelectListItem>() //creat the drop down list for date filters
        {
            new SelectListItem() {Text="All", Value= DateTime.Now.AddYears(-20).ToString()},
            new SelectListItem() {Text="Previous Month", Value= DateTime.Now.AddMonths(-1).ToString()},
            new SelectListItem() {Text="Previous 6 Months", Value= DateTime.Now.AddMonths(-6).ToString()},
            new SelectListItem() {Text="Previous Year", Value= DateTime.Now.AddYears(-1).ToString()},
        };

        public PressClipping GetItems(int Like_ID = 0, DateTime? filter_date = null, int Page = 0)
        {
            filter_date = filter_date == null ? DateTime.Now.AddYears(-20) : filter_date; //fix null dates so db doesnt error out
            PressClipping pc = new PressClipping();
            pc.Items = new List<Press_Clippings>();
            using(KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                pc.Items = db.Press_Clippings
                    .Where(m => m.enabled == true && (m.Kenmark_Collections_Like_ID == Like_ID || Like_ID == 0) && m.release_date > filter_date)
                    .OrderByDescending( m => m.release_date)
                    .Skip(Page * 9)
                    .Take(9)
                    .ToList();
            }
            return pc;
        }
    }
   
}