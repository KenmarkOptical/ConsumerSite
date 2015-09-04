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

    }
}
