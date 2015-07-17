using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class StyleController : Controller
    {
        //
        // GET: /Style/

        public ActionResult Index()
        {
            Style s = new Style().GetStyle("AMBL");
            return View();
        }

    }
}
