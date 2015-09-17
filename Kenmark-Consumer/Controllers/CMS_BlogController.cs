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
                // image1 = Image.FromStream(main_image.InputStream, true, true) ; // saving these variables incase I find a better way to 
                //var imageDimension = image1.PhysicalDimension;                   // save images to allow changing image properties
                //var imageHeight = image1.Height;
                //var imageWidth = image1.Width;
                var fileName = Path.GetFileName(main_image.FileName);
                var imagePath = (Path.Combine(directory, fileName));

                main_image.SaveAs(imagePath);
                blog.data.main_image = imagePath.Replace("C:\\Users\\telkins\\Source\\Repos\\ConsumerSite\\Kenmark-Consumer","");
                
                blog.main_image = main_image;
            }
           
            if (sub_image != null && sub_image.ContentLength > 0)
            {
                //image2 = Image.FromStream(sub_image.InputStream);
                //var imageDimension = image2.PhysicalDimension;
                //var imageHeight = image2.Height;
                //var imageWidth = image2.Width;
                var fileName = Path.GetFileName(sub_image.FileName);
                var imagePath = (Path.Combine(directory, fileName));

                sub_image.SaveAs(imagePath);
                blog.data.sub_image = imagePath;
                blog.sub_image = sub_image;
            }

            blog.data.enabled = true;
            
            if (ModelState.IsValid)
            {
                bool result = a.AddBlog(blog);

                a.AddBlog(blog);

                return Json(new { successs = true });
            }
            
            else {

                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(new { success = false });
            }
         }
    }
}
