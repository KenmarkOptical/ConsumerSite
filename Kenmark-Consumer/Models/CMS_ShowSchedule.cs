using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class CMS_ShowSchedule
    {
        public List<@event> Shows { get; set; }
        public @event Show { get; set; }

        public CMS_ShowSchedule GetShows()
        {
            CMS_ShowSchedule s = new CMS_ShowSchedule();
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Shows = db.events.Where(m => m.is_active == true).ToList();
            }
            return s;
        }

        public void AddShow(CMS_ShowSchedule s)
        {            
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Show.is_active = true;
                s.Show.insert_date = DateTime.Now;
                db.events.Add(s.Show);
                db.SaveChanges();
            }            
        }

        public void DeleteShow(int id)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var show = db.events.Where(m => m.event_id == id).FirstOrDefault();
                show.is_active = false;
                db.SaveChanges();
            }
        }

        public CMS_ShowSchedule GetEditShow(int id)
        {
            CMS_ShowSchedule s = new CMS_ShowSchedule();
            s = s.GetShows();
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Show = db.events.Where(m => m.event_id == id).FirstOrDefault();                
            }
            return s;
        }


        public void SaveEditShow(CMS_ShowSchedule s)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {                
                var show = db.events.Where(m => m.event_id == s.Show.event_id).FirstOrDefault();
                show.event_name = s.Show.event_name;
                show.display_date = s.Show.display_date;
                show.event_start_date = s.Show.event_start_date;
                show.location = s.Show.location;
                show.is_active = true;
                show.Booth = s.Show.Booth;

                db.SaveChanges();
            }
        }
    }

}