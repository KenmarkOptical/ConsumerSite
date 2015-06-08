using System;
using System.Collections.Generic;
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
                                   .Select(m => new Show() { EventName = m.event_name, Date = m.display_date, Booth = m.Booth, Group = m.event_start_date.ToString("MMM") + " " + m.event_start_date.Year})
                                   .ToList();
            }
            return s;
        }
    }

    public class Show
    {
        public string EventName { get; set; }
        public string Date { get; set; }
        public string Booth { get; set; }
        public string Group { get; set; }
    }
}