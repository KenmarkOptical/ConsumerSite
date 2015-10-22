using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class CMS_ShowScheduleController : Controller
    {
        //
        // GET: /CMS_ShowSchedule/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            CMS_ShowSchedule s = new CMS_ShowSchedule();
            ViewBag.Type = "ADD";
            return View("Form",s);
        }

        public ActionResult Delete()
        {
            CMS_ShowSchedule s = new CMS_ShowSchedule();
            s = s.GetShows();
            ViewBag.Type = "DELETE";
            return View("Form", s);
        }

        public ActionResult Edit()
        {
            CMS_ShowSchedule s = new CMS_ShowSchedule();
            s = s.GetShows();
            ViewBag.Type = "Edit";
            return View("Form", s);
        }

        public ActionResult AddShow(CMS_ShowSchedule s)
        {
            s.AddShow(s);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteShow(int delete_id)
        {
            CMS_ShowSchedule s = new CMS_ShowSchedule();
            s.DeleteShow(delete_id);
            return RedirectToAction("Index");
        }

        public ActionResult EditShow(int edit_id)
        {
            CMS_ShowSchedule s = new CMS_ShowSchedule();
            s = s.GetEditShow(edit_id);
            ViewBag.Type = "EDIT2";
            return PartialView("Form", s);
        }

        public ActionResult SaveEditShow(CMS_ShowSchedule s)
        {
            s.SaveEditShow(s);
            return RedirectToAction("Index");
        }
    }
}
