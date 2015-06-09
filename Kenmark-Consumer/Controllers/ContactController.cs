using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            Contact c = new Contact();
            return View(c);
        }

        public ActionResult SaveContact(Contact c)
        {
           bool result =  c.SaveContact(c);
           return Json(new { success = result });
        }

    }
}
