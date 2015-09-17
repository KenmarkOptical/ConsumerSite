using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Kenmark_Consumer.Controllers
{
    public class CMS_Blog_EditController : MyBaseController
    {
        //
        // GET: /CMS_Blog_Edit/

        public ActionResult Index()
        {
            CMS_Blog b = new CMS_Blog().GetBlogs();
            return View(b);
        }

     
        public ActionResult GetBlog(int blog_id)
        {            
            CMS_Blog a = new CMS_Blog();
            SingleBlog b = new SingleBlog();
            b = a.GetBlog(blog_id);
                     
            string html = RenderPartialViewToString("_Grid", b);
            return Json(new { html = html }, JsonRequestBehavior.AllowGet);
        }

       

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditBlog(SingleBlog blog)
        {
            return RedirectToAction("Index");
        }

    
    }
}
