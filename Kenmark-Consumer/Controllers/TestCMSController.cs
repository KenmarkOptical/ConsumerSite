using Kenmark_Consumer.Models;
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

        [HttpPost]
        public ActionResult UpdateContent(TestCMS t)
        {
            return View();
        }

    }
}
