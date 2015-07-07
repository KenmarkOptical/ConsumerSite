using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class PressReleaseController : Controller
    {
        //
        // GET: /PressRelease/

        public ActionResult Index()
        {
            PressRelease p = new PressRelease().GetItems();
            return View(p);
        }

    }
}
