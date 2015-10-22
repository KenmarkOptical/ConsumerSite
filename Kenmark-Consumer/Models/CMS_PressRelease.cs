using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class CMS_PressRelease
    {
        public List<press_releases> Releases { get; set; }
        public press_releases Release { get; set; }

        public CMS_PressRelease GetShows()
        {
            CMS_PressRelease s = new CMS_PressRelease();
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Releases = db.press_releases.Where(m => m.active == true).ToList();
            }
            return s;
        }

        public void AddShow(CMS_PressRelease s)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Release.active = true;
                s.Release.insert_date = DateTime.Now;
                db.press_releases.Add(s.Release);
                db.SaveChanges();
            }
        }

        public void DeleteShow(int id)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var r = db.press_releases.Where(m => m.press_release_id == id).FirstOrDefault();
                r.active = false;
                db.SaveChanges();
            }
        }

        public CMS_PressRelease GetEditShow(int id)
        {
            CMS_PressRelease s = new CMS_PressRelease();
            s = s.GetShows();
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Release = db.press_releases.Where(m => m.press_release_id == id).FirstOrDefault();
            }
            return s;
        }

        
        public void SaveEditShow(CMS_PressRelease s)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var r = db.press_releases.Where(m => m.press_release_id == s.Release.press_release_id).FirstOrDefault();
                r.active = true;
                r.release_date = s.Release.release_date;
                r.release_title = s.Release.release_title;
                r.release_text = s.Release.release_text;
                r.insert_date = DateTime.Now;

                db.SaveChanges();
            }
        }
    }

}