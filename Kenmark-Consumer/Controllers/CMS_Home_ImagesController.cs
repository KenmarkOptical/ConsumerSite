using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Kenmark_Consumer.Controllers
{
    public class CMS_Home_ImagesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            CMS_Home_Image_Class s = new CMS_Home_Image_Class();
            s = s.GetShows();
            ViewBag.Type = "Edit";
            return View("Form", s);
        }                 


        [ValidateInput(false)]
        public ActionResult EditShow(int edit_id)
        {
            CMS_Home_Image_Class s = new CMS_Home_Image_Class();
            s = s.GetEditShow(edit_id);
            ViewBag.Type = "EDIT2";
            return PartialView("Form", s);
        }

        [ValidateInput(false)]
        public ActionResult SaveEditShow(HttpPostedFileBase main_image, CMS_Home_Image_Class s)
        {
            string directory = Server.MapPath("~/Content/images/home_carousel");
            string error_msg = "";

            if (main_image != null && main_image.ContentLength > 0)
            {
                if (s.Release.block_size == 2)
                {
                    List<string> Errors = Common.CheckImage(main_image, "main image", 620, 225);
                    if (Errors.Count > 0)
                    {
                        foreach (var item in Errors)
                        {
                            error_msg += item + " <br /> ";
                        }
                    }
                    else
                    {
                        s.Release.image = Common.SaveImage(main_image, directory);
                    }
                }
                else
                {
                    List<string> Errors = Common.CheckImage(main_image, "main image", 310, 225);
                    if (Errors.Count > 0)
                    {
                        foreach (var item in Errors)
                        {
                            error_msg += item + " <br /> ";
                        }
                    }
                    else
                    {
                        s.Release.image = Common.SaveImage(main_image, directory);
                    }
                }
                

            }

            if (!string.IsNullOrEmpty(error_msg))
            {
                ViewBag.Error = error_msg;
                ViewBag.Type = "EDIT2";
                return View("Form", s);
            }

            s.SaveEditShow(s);
            return RedirectToAction("Index");
        }
    }
}

