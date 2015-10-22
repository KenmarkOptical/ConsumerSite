using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class CMS_PressController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Add()
        {
            CMS_Press s = new CMS_Press().GetShows();
            ViewBag.Type = "ADD";
            return View("Form", s);
        }

        public ActionResult Delete()
        {
            CMS_Press s = new CMS_Press();
            s = s.GetShows();
            ViewBag.Type = "DELETE";
            return View("Form", s);
        }

        public ActionResult Edit()
        {
            CMS_Press s = new CMS_Press();
            s = s.GetShows();
            ViewBag.Type = "Edit";
            return View("Form", s);
        }

        [ValidateInput(false)]
        public ActionResult AddShow(HttpPostedFileBase main_image, HttpPostedFileBase inside_image, CMS_Press s)
        {
            
            string directory = Server.MapPath("~/Content/images/Press_Clippings");
            string error_msg = "";

            if (main_image != null && main_image.ContentLength > 0)
            {
                List<string> Errors = Common.CheckImage(main_image, "main image", 150, 200);
                if (Errors.Count > 0)
                {             
                    foreach (var item in Errors)
                    {
                        error_msg += item + " <br /> ";       
                    }                   
                }
                else
                {
                    s.Release.main_image = Common.SaveImage(main_image, directory);
                }
               
            }


            if (inside_image != null && inside_image.ContentLength > 0)
            {
                List<string> Errors = Common.CheckImage(inside_image, "inside image", 0, 0);
                if (Errors.Count > 0)
                {                    
                    foreach (var item in Errors)
                    {
                        error_msg += item + " <br /> ";
                    }                   
                }
                else
                {
                    s.Release.inside_image = Common.SaveImage(inside_image, directory);
                }

            }

            if (!string.IsNullOrEmpty(error_msg))
            {
                ViewBag.Error = error_msg;
                ViewBag.Type = "ADD";
                var s2 = s.GetShows();
                s.Collections = s2.Collections;
                return View("Form", s);
            }           

            s.AddShow(s);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteShow(int delete_id)
        {
            CMS_Press s = new CMS_Press();
            s.DeleteShow(delete_id);
            return RedirectToAction("Index");
        }

        [ValidateInput(false)]
        public ActionResult EditShow(int edit_id)
        {
            CMS_Press s = new CMS_Press();
            s = s.GetEditShow(edit_id);
            ViewBag.Type = "EDIT2";
            return PartialView("Form", s);
        }

        [ValidateInput(false)]
        public ActionResult SaveEditShow(HttpPostedFileBase main_image, HttpPostedFileBase inside_image, CMS_Press s)
        {
            string directory = Server.MapPath("~/Content/images/Press_Clippings");
            string error_msg = "";

            if (main_image != null && main_image.ContentLength > 0)
            {
                List<string> Errors = Common.CheckImage(main_image, "main image", 150, 200);
                if (Errors.Count > 0)
                {
                    foreach (var item in Errors)
                    {
                        error_msg += item + " <br /> ";
                    }
                }
                else
                {
                    s.Release.main_image = Common.SaveImage(main_image, directory);
                }

            }


            if (inside_image != null && inside_image.ContentLength > 0)
            {
                List<string> Errors = Common.CheckImage(inside_image, "inside image", 0, 0);
                if (Errors.Count > 0)
                {
                    foreach (var item in Errors)
                    {
                        error_msg += item + " <br /> ";
                    }
                }
                else
                {
                    s.Release.inside_image = Common.SaveImage(inside_image, directory);
                }

            }

            if (!string.IsNullOrEmpty(error_msg))
            {
                ViewBag.Error = error_msg;
                ViewBag.Type = "ADD";
                var s2 = s.GetShows();
                s.Collections = s2.Collections;
                return View("Form", s);
            }

            s.SaveEditShow(s);
            return RedirectToAction("Index");
        }
    }
}
