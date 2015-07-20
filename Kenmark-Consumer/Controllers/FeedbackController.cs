using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class FeedbackController : Controller
    {
        //
        // GET: /Feedback/

        public ActionResult Index(string style)
        {
            Feedback f = new Feedback();
            f.Style = style;
            return View(f);
        }

        [HttpPost]
        public ActionResult SaveFeedback(Feedback f)
        {
            //f.SaveFeedback(f);
            return View();
        }
    }
}
