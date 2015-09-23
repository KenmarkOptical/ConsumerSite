using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kenmark_Consumer.Models;
namespace Kenmark_Consumer.Controllers
{
    public class TheMirrorController : Controller
    {
        //
        // GET: /TheMirror/

        public ActionResult Index()
        {
            CMS_Blog b = new CMS_Blog().GetBlogs();
            return View(b);
        }

        public ActionResult SingleBlog(int id)
        {
            CMS_Blog a = new CMS_Blog();
            SingleBlog b = new SingleBlog();

            b = a.GetBlog(id);
            b.main_image = b.main_image;
            b.sub_image = b.sub_image;
            

            return View("SingleBlog", b);
        }

    }
}
