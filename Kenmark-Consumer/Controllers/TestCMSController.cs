using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class TestCMSController : Controller
    {
        //
        // GET: /TestCMS/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateContent(String title, String date, String story, String storyImage)
        {
            return View();
        }

    }
}
