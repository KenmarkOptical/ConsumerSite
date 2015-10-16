using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kenmark_Consumer.Models;

namespace Kenmark_Consumer.Controllers
{
    public class CMS_PressReleaseController : Controller
    {
        //
        // GET: /CMS_PressRelease/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPressReleaseIndex()
        {
            return View("Add");
        }

        public ActionResult DeletePressReleaseIndex()
        {
            
            return View("Delete");
        }

        public ActionResult EditPressReleaseIndex()
        {
            
            return View("Edit");
        }

    }
}
