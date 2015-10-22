using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class CMS_Blog_DeleteController : Controller
    {
        //
        // GET: /CMS_Blog_Delete/

        public ActionResult Index()
        {
            CMS_Blog b = new CMS_Blog().GetBlogs();
            return View(b);
        }

        [HttpPost]
        public ActionResult DeleteBlog(int id)
        {
            CMS_Blog a = new CMS_Blog();
            a.DeleteBlog(id);
            return RedirectToAction("Index");
        }


       
    }
}
