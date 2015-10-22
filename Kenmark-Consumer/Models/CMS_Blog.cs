using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;


namespace Kenmark_Consumer.Models
{
    public class CMS_Blog
    {
        public List<CMS_Blogs> Items { get; set; }
        public CMS_Blogs data { get; set; }

        public CMS_Blog GetBlogs()
        {
            CMS_Blog cb = new CMS_Blog();
            cb.Items = new List<CMS_Blogs>();
           
            using(KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                cb.Items = db.CMS_Blogs.Where(m => m.enabled == true && m.date <= DateTime.Now)
                    .OrderByDescending(m => m.date)
                    .ToList();

            }
            return cb;
        }

        public CMS_Blog GetBlog(int id)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                CMS_Blog sb = new CMS_Blog();
                sb.data = db.CMS_Blogs.Where(m => m.id == id).FirstOrDefault();
                return sb;
            }
        }

        public bool AddBlog(CMS_Blog blog)
        {
            using(KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                blog.data.enabled = true;
                db.CMS_Blogs.Add(blog.data);
                db.SaveChanges();
            }
            return true;
        }

        public void DeleteBlog(int id)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var item = db.CMS_Blogs.Where(m => m.id == id).FirstOrDefault();
                item.enabled = false;
                db.SaveChanges();
            }
        }

        public void EditBlog(CMS_Blog blog)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {             
                var item = db.CMS_Blogs.Where(m => m.id == blog.data.id).FirstOrDefault();
                item.date = blog.data.date;
                if (blog.data.main_image != null)
                {
                    item.main_image = blog.data.main_image;
                }
                
                item.title = blog.data.title;

                if (blog.data.sub_image != null)
                {
                    item.sub_image = blog.data.sub_image;
                }
                
                item.sub_image_caption = blog.data.sub_image_caption;
                item.text = blog.data.text;
                item.enabled = true;
                db.SaveChanges();
            }
            
        }

    }


}