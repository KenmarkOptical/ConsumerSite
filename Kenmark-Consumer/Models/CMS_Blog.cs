using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class CMS_Blog
    {
       public List<SingleBlog> Items { get; set; }

        public CMS_Blog GetBlogs()
        {
            CMS_Blog cb = new CMS_Blog();
            using(KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                cb.Items = db.CMS_Blogs.Where(m => m.enabled == true && m.date <= DateTime.Now)
                    .OrderByDescending(m => m.date)
                    .Select(m => new SingleBlog{ data = m})
                    .ToList();
            }
            return cb;
        }

        public SingleBlog GetBlog(int id)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                SingleBlog sb = new SingleBlog();
                sb.data = db.CMS_Blogs.Where(m => m.id == id).FirstOrDefault();
                return sb;
            }
        }

        public void AddBlog(SingleBlog blog)
        {
            using(KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                db.CMS_Blogs.Add(blog.data);
                //db.SaveChanges();
            }
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

        public void EditBlog(SingleBlog blog)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var item = db.CMS_Blogs.Where(m => m.id == blog.data.id).FirstOrDefault();
                item.date = blog.data.date;
                item.main_image = blog.data.main_image;
                item.title = blog.data.title;
                item.sub_image = blog.data.sub_image;
                item.sub_image_caption = blog.data.sub_image_caption;
                item.text = blog.data.text;
                item.enabled = blog.data.enabled;
                db.SaveChanges();
            }
        }

    }

    public class SingleBlog
    {
        public CMS_Blogs data { get; set; }
        public Image main_image { get; set; }
        public Image sub_image { get; set; }
    }
}