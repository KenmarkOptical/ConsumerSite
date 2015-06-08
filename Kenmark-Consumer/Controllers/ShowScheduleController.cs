using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class ShowScheduleController : Controller
    {
        //
        // GET: /ShowSchedule/

        public ActionResult Index()
        {
            ShowSchedule s = new ShowSchedule().GetShow();

            //change to return View(s) when ready to test.
            return View(s);
        }

    }
}
