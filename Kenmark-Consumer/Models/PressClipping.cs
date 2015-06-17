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
        public int? Filter_Like_Collection { get; set; }
        public DateTime? Filter_DateRange { get; set; }
        public List<PressClippingItem> Items { get; set; }
        public bool HasNextPage { get; set; }

        public static List<SelectListItem> Date_List = new List<SelectListItem>() //creat the drop down list for date filters
        {
            new SelectListItem() {Text="All Dates", Value= DateTime.Now.AddYears(-20).ToString()},
            new SelectListItem() {Text="Previous Month", Value= DateTime.Now.AddMonths(-1).ToString()},
            new SelectListItem() {Text="Previous 6 Months", Value= DateTime.Now.AddMonths(-6).ToString()},
            new SelectListItem() {Text="Previous Year", Value= DateTime.Now.AddYears(-1).ToString()},
        };

        public PressClipping GetItems(int? Like_ID = 0, DateTime? filter_date = null, int Page = 0)
        {
            filter_date = filter_date == null ? DateTime.Now.AddYears(-20) : filter_date; //fix null dates so db doesnt error out
            Like_ID = Like_ID == null ? 0 : Like_ID;
            PressClipping pc = new PressClipping();
            pc.Filter_Like_Collection = Like_ID;
            pc.Filter_DateRange = filter_date;
            pc.Page = Page;
            pc.Items = new List<PressClippingItem>();
            using(KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                //pc.Items = db.Press_Clippings
                //    .Where(m => m.enabled == true && (m.Kenmark_Collections_Like_ID == Like_ID || Like_ID == 0) && m.release_date > filter_date)
                //    .OrderByDescending( m => m.release_date)
                //    .Skip(Page * 9)
                //    .Take(9)
                //    .ToList(); 
                
                pc.Items = (from p in db.Press_Clippings
                            join l in db.Kenmark_Collections_like on p.Kenmark_Collections_Like_ID equals l.ID
                            where p.enabled == true && (p.Kenmark_Collections_Like_ID == Like_ID || Like_ID == 0) && p.release_date > filter_date
                            select new PressClippingItem(){ press_clipping = p, Collection = l.Site_Display})
                            .OrderByDescending(m => m.press_clipping.release_date)
                            .Skip(Page * 9)
                            .Take(9)
                            .ToList(); 

                   pc.HasNextPage = ((Page + 1) * 9) < 
                                  (db.Press_Clippings.Where(m => m.enabled == true && (m.Kenmark_Collections_Like_ID == Like_ID || Like_ID == 0) && m.release_date > filter_date).Count()) ? true : false;
                   
                  
            }
            return pc;
        }
    }

    public class PressClippingItem
    {
        public Press_Clippings press_clipping { get; set; }
        public string Collection { get; set; }
    }
   
}