using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class PressClippingController : MyBaseController
    {
        //
        // GET: /PressClipping/

        public ActionResult Index(PressClipping p)
        {
            p = new PressClipping().GetItems(p.Filter_Like_Collection, p.Filter_DateRange, p.Page);                
            return View(p);
        }

        public ActionResult UpdateData(PressClipping p)
        {
            p = new PressClipping().GetItems(p.Filter_Like_Collection, p.Filter_DateRange, p.Page);
            return Json(new { html = RenderPartialViewToString("_Grid", p) });
        }

    }
}
