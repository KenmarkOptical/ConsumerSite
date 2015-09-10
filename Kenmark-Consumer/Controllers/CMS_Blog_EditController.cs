using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class CMS_Blog_EditController : Controller
    {
        //
        // GET: /CMS_Blog_Edit/

        public ActionResult Index()
        {
            CMS_Blog b = new CMS_Blog().GetBlogs();
            return View(b);
        }

        [HttpPost]
        public ActionResult GetBlog(int id)
        {
            CMS_Blog a = new CMS_Blog();
            SingleBlog b = new SingleBlog();

           b = a.GetBlog(id);

           return Json(new { data = b, }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditBlog(SingleBlog blog)
        {
            return RedirectToAction("Index");
        }

    
    }
}
