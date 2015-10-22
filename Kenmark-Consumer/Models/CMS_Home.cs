using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Models
{
    public class CMS_Home
    {
    }

    public class CMS_Home_Image_Class
    {
        public List<CMS_Home_Images> Releases { get; set; }
        public CMS_Home_Images Release { get; set; }
        public List<SelectListItem> Order = new List<SelectListItem>();

        public CMS_Home_Image_Class GetShows(bool is_edit = false)
        {
            CMS_Home_Image_Class s = new CMS_Home_Image_Class();
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Releases = db.CMS_Home_Images.Where(m => m.active == true).ToList();
            }
            int counter = 0;
            foreach (var item in s.Releases)
            {
                counter++;
                s.Order.Add(new SelectListItem() { Text = counter.ToString(), Value = counter.ToString() });
            }

            if (is_edit == false)
            {
                s.Order.Add(new SelectListItem() { Text = (counter + 1).ToString(), Value = (counter + 1).ToString() });
            }

            return s;
        }   

       

        public CMS_Home_Image_Class GetEditShow(int id)
        {
            CMS_Home_Image_Class s = new CMS_Home_Image_Class();
            s = s.GetShows(true);
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Release = db.CMS_Home_Images.Where(m => m.id == id).FirstOrDefault();
            }
            return s;
        }


        public void SaveEditShow(CMS_Home_Image_Class s)
        {
            s.Release.rank -= 1;

            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                //fix the ordering
                var b = db.CMS_Home_Images.Where(m => m.active == true && m.id != s.Release.id).OrderBy(m => m.rank).ToList();
                b.Insert((int)s.Release.rank, s.Release);
                for (int i = 0; i < b.Count; i++)
                {
                    var id = b[i].id;
                    var item = db.CMS_Home_Images.Where(m => m.id == id).FirstOrDefault();
                    if (item != null && item.id != s.Release.id)
                        item.rank = i;
                }

                var r = db.CMS_Home_Images.Where(m => m.id == s.Release.id).FirstOrDefault();
                r.active = true;
                r.rank = s.Release.rank;
                r.link = s.Release.link;
                r.main_text = s.Release.main_text;
                r.sub_text = s.Release.sub_text;


                if (!String.IsNullOrEmpty(s.Release.image))
                {
                    r.image = s.Release.image;
                }

                db.SaveChanges();
            }
        }
    }
    

    public class CMS_Home_CarouselClass
    {
        public List<CMS_Home_Carousel> Releases { get; set; }
        public CMS_Home_Carousel Release { get; set; }
        public List<SelectListItem> Order = new List<SelectListItem>();

        public CMS_Home_CarouselClass GetShows(bool is_edit = false)
        {
            CMS_Home_CarouselClass s = new CMS_Home_CarouselClass();
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Releases = db.CMS_Home_Carousel.Where(m => m.enabled == true).ToList();
            }
            int counter = 0;
            foreach (var item in s.Releases)
            {
                counter++;
                s.Order.Add(new SelectListItem() { Text = counter.ToString(), Value = counter.ToString() });
            }

            if (is_edit == false)
            {
                s.Order.Add(new SelectListItem() { Text = (counter + 1).ToString(), Value = (counter + 1).ToString() });
            }

            return s;
        }

        public void AddShow(CMS_Home_CarouselClass s)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Release.rank -= 1;

                //fix the ordering
                var b = db.CMS_Home_Carousel.Where(m => m.enabled == true).OrderBy(m => m.rank).ToList();
                b.Insert((int)s.Release.rank, s.Release);
                for (int i = 0; i < b.Count; i++)
                {
                    var id = b[i].id;
                    var item = db.CMS_Home_Carousel.Where(m => m.id == id).FirstOrDefault();
                    if(item != null)
                        item.rank = i;
                }

                s.Release.enabled = true;                
                db.CMS_Home_Carousel.Add(s.Release);
                db.SaveChanges();
            }
        }

        public void DeleteShow(int id)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var r = db.CMS_Home_Carousel.Where(m => m.id == id).FirstOrDefault();
                r.enabled = false;
                r.rank = null;
                db.SaveChanges();
            }
        }

        public CMS_Home_CarouselClass GetEditShow(int id)
        {
            CMS_Home_CarouselClass s = new CMS_Home_CarouselClass();
            s = s.GetShows(true);
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                s.Release = db.CMS_Home_Carousel.Where(m => m.id == id).FirstOrDefault();
            }
            return s;
        }


        public void SaveEditShow(CMS_Home_CarouselClass s)
        {
            s.Release.rank -= 1;

            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                //fix the ordering
                var b = db.CMS_Home_Carousel.Where(m => m.enabled == true && m.id != s.Release.id).OrderBy(m => m.rank).ToList();
                b.Insert((int)s.Release.rank, s.Release);
                for (int i = 0; i < b.Count; i++)
                {
                    var id = b[i].id;
                    var item = db.CMS_Home_Carousel.Where(m => m.id == id).FirstOrDefault();
                    if (item != null && item.id != s.Release.id)
                        item.rank = i;
                }

                var r = db.CMS_Home_Carousel.Where(m => m.id == s.Release.id).FirstOrDefault();
                r.enabled = true;
                r.rank = s.Release.rank;
                r.link = s.Release.link;

                if (!String.IsNullOrEmpty(s.Release.image))
                {
                    r.image = s.Release.image;
                }

                db.SaveChanges();
            }
        }
    }
}