using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class ShowSchedule
    {
        public List<Show> Shows { get; set; }

        public ShowSchedule GetShow()
        {
            ShowSchedule s = new ShowSchedule();
            s.Shows = new List<Show>();
            

            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {

                s.Shows = db.events.Where(m => m.is_active == true)
                                   .Select(m => new Show() { EventName = m.event_name, Date = m.display_date, Booth = m.Booth, Month = m.event_start_date.Month, Year = m.event_start_date.Year, Location = m.location, start_date = m.event_start_date})
                                   .OrderByDescending(m => m.start_date)
                                   .ToList();
            }

            //fix the month abreviations for the groupings
            foreach (var item in s.Shows)
            {
                string monthName = new DateTime(item.Year, item.Month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                item.Group = monthName + " " + item.Year;
            }
            return s;
        }
        
    }

    public class Show
    {
        public string EventName { get; set; }
        public string Date { get; set; }
        public string Booth { get; set; }
        public string Location { get; set; }
        public string Group { get; set; }       
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime? start_date { get; set; }
        
    }

    
}