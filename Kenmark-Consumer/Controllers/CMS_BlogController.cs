using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class CMS_BlogController : Controller
    {
        //
        // GET: /CMS_Blog/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddBlog(HttpPostedFileBase main_image, HttpPostedFileBase sub_image, SingleBlog blog)
        {
            CMS_Blog a = new CMS_Blog();
            
            
            
            
            string directory = Server.MapPath("~/Content/images/TheMirror");


            if (main_image != null && main_image.ContentLength > 0)
            {
                Image image = Image.FromStream(main_image.InputStream);

                var fileName = blog.data.title + "_main_image" + ".jpg";
                var imageDimension = image.PhysicalDimension;
                var imageHeight = image.Height;
                var imageWidth = image.Width;
                var imagePath = (Path.Combine(directory, fileName));

                main_image.SaveAs(imagePath);
                blog.data.main_image = imagePath;

            }

            if (sub_image != null && sub_image.ContentLength > 0)
            {
                Image image = Image.FromStream(sub_image.InputStream);

                var fileName = blog.data.title + "_sub_image" + ".jpg";
                var imageDimension = image.PhysicalDimension;
                var imageHeight = image.Height;
                var imageWidth = image.Width;
                var imagePath = (Path.Combine(directory, fileName));


                sub_image.SaveAs(imagePath);
                blog.data.sub_image = imagePath;

            }

            blog.data.enabled = true;
            a.AddBlog(blog);
           

            
            
            
            return RedirectToAction("Index");
        }

 
    }
}
