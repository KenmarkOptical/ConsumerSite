using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class CMS_PressReleaseController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

     
        public ActionResult Add()
        {
            CMS_PressRelease s = new CMS_PressRelease();
            ViewBag.Type = "ADD";
            return View("Form", s);
        }

        public ActionResult Delete()
        {
            CMS_PressRelease s = new CMS_PressRelease();
            s = s.GetShows();
            ViewBag.Type = "DELETE";
            return View("Form", s);
        }

        public ActionResult Edit()
        {
            CMS_PressRelease s = new CMS_PressRelease();
            s = s.GetShows();
            ViewBag.Type = "Edit";
            return View("Form", s);
        }

        [ValidateInput(false)]
        public ActionResult AddShow(CMS_PressRelease s)
        {
            s.AddShow(s);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteShow(int delete_id)
        {
            CMS_PressRelease s = new CMS_PressRelease();
            s.DeleteShow(delete_id);
            return RedirectToAction("Index");
        }

        [ValidateInput(false)]
        public ActionResult EditShow(int edit_id)
        {
            CMS_PressRelease s = new CMS_PressRelease();
            s = s.GetEditShow(edit_id);
            ViewBag.Type = "EDIT2";
            return PartialView("Form", s);
        }

         [ValidateInput(false)]
        public ActionResult SaveEditShow(CMS_PressRelease s)
        {
            s.SaveEditShow(s);
            return RedirectToAction("Index");
        }
    }
}
