using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Models
{
    public class CMS_Press
    {
        public List<Press_Clippings> Releases { get; set; }
        public Press_Clippings Release { get; set; }
        public List<SelectListItem> Collections = new List<SelectListItem>();

        public CMS_Press GetShows()
        {
            CMS_Press s = new CMS_Press();
            s.Collections = new List<SelectListItem>();
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Releases = db.Press_Clippings.Where(m => m.enabled == true).ToList();
                var coll = db.Kenmark_Collections_like.Where(m => m.Enabled == true).ToList();
                foreach (var item in coll)
                {
                    s.Collections.Add(new SelectListItem() { Text = item.Site_Display, Value = item.ID.ToString() });
                }
                s.Collections = s.Collections.OrderBy(m => m.Text).ToList();
            }
            return s;
        }

        public void AddShow(CMS_Press s)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Release.enabled = true;          
                db.Press_Clippings.Add(s.Release);
                db.SaveChanges();
            }
        }

        public void DeleteShow(int id)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var r = db.Press_Clippings.Where(m => m.id == id).FirstOrDefault();
                r.enabled = false;
                db.SaveChanges();
            }
        }

        public CMS_Press GetEditShow(int id)
        {
            CMS_Press s = new CMS_Press();
            s.Collections = new List<SelectListItem>();
            s = s.GetShows();
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Release = db.Press_Clippings.Where(m => m.id == id).FirstOrDefault();
                var coll = db.Kenmark_Collections_like.Where(m => m.Enabled == true).ToList();
                foreach (var item in coll)
                {
                    s.Collections.Add(new SelectListItem() { Text = item.Site_Display, Value = item.ID.ToString() });
                }

                s.Collections = s.Collections.OrderBy(m => m.Text).ToList();
            }
            return s;
        }


        public void SaveEditShow(CMS_Press s)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var r = db.Press_Clippings.Where(m => m.id == s.Release.id).FirstOrDefault();
                r.enabled = true;
                r.Kenmark_Collections_Like_ID = s.Release.Kenmark_Collections_Like_ID;
                r.release_date = s.Release.release_date;
                r.magazine = s.Release.magazine;
                r.main_image = s.Release.main_image;
                r.inside_image = s.Release.inside_image;
                r.frame = s.Release.frame;
                r.pdf = s.Release.pdf;
                db.SaveChanges();
            }
        }
    }

}