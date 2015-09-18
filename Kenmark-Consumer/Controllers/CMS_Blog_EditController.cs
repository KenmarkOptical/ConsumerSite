//using Kenmark_Consumer.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Script.Serialization;

//namespace Kenmark_Consumer.Controllers
//{
//    public class CMS_Blog_EditController : MyBaseController
//    {
//        //
//        // GET: /CMS_Blog_Edit/

//        public ActionResult Index()
//        {
//            CMS_Blog b = new CMS_Blog().GetBlogs();
//            return View(b);
//        }

//        public ActionResult GetBlog(int blog_id)
//        {            
//            CMS_Blog a = new CMS_Blog();
//            SingleBlog b = new SingleBlog();
//            b = a.GetBlog(blog_id);
                     
//            string html = RenderPartialViewToString("_Grid", b);
//            return Json(new { html = html }, JsonRequestBehavior.AllowGet);
//        }

//        [HttpPost]
//        [ValidateInput(false)]
//        public ActionResult EditBlog(HttpPostedFileBase main_image, HttpPostedFileBase sub_image, SingleBlog blog)
//        {
//            CMS_Blog a = new CMS_Blog();

//            string directory = Server.MapPath("~/Content/images/TheMirror");


//            if (main_image != null && main_image.ContentLength > 0)
//            {
//                var fileName = Path.GetFileName(main_image.FileName);
//                var imagePath = (Path.Combine(directory, fileName));
//                main_image.SaveAs(imagePath);
//                blog.data.main_image = imagePath.Replace("C:\\Users\\telkins\\Source\\Repos\\ConsumerSite\\Kenmark-Consumer", "");
//                blog.main_image = main_image;
//            }

//            if (sub_image != null && sub_image.ContentLength > 0)
//            {
//                var fileName = Path.GetFileName(sub_image.FileName);
//                var imagePath = (Path.Combine(directory, fileName));
//                sub_image.SaveAs(imagePath);
//                blog.data.sub_image = imagePath;
//                blog.sub_image = sub_image;
//            }
            
//            if (ModelState.IsValid)
//            {
//                 a.EditBlog(blog);
//            }

//            return RedirectToAction("Index");
//        }
//    }
//}
