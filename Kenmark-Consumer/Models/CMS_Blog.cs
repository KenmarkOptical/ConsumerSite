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
            cb.Items = new List<SingleBlog>();
           
            using(KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                cb.Items = db.CMS_Blogs.Where(m => m.enabled == true && m.date <= DateTime.Now)
                    .OrderByDescending(m => m.date)
                    .Select(m => new SingleBlog { data = m })
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

        public bool AddBlog(SingleBlog blog)
        {
            using(KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var test = db.CMS_Blogs.ToList();
                db.CMS_Blogs.Add(blog.data);
                //db.SaveChanges();
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

        public void EditBlog(SingleBlog blog)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var item = db.CMS_Blogs.Where(m => m.id == blog.data.id).FirstOrDefault();
                item.date = blog.data.date;
                if (item.main_image == null)
                {
                    item.main_image = blog.data.main_image;
                }
                
                item.title = blog.data.title;

                if (item.sub_image == null)
                {
                    item.sub_image = blog.data.sub_image;
                }
                
                item.sub_image_caption = blog.data.sub_image_caption;
                item.text = blog.data.text;
                item.enabled = blog.data.enabled;
                //db.SaveChanges();
            }
            
        }

    }

    public class SingleBlog
    {
        public CMS_Blogs data { get; set; }
        public HttpPostedFileBase main_image { get; set; }
        public HttpPostedFileBase sub_image { get; set; }
    }
}