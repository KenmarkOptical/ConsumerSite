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

        [HttpPost]
        public ActionResult SaveContact(Contact c)
        {
            if (ModelState.IsValid)
            {
                System.Threading.Thread.Sleep(5000);
                // bool result =  c.SaveContact(c);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }

          
            
        }

    }
}
