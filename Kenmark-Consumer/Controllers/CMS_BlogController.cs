using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Kenmark_Consumer.Controllers
{
    public class CMS_BlogController : MyBaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            CMS_Blog s = new CMS_Blog();
            ViewBag.Type = "ADD";
            return View("Form", s);
        }

        public ActionResult Delete()
        {
            CMS_Blog s = new CMS_Blog();
            s = s.GetBlogs();
            ViewBag.Type = "DELETE";
            return View("Form", s);
        }

        public ActionResult Edit()
        {
            CMS_Blog s = new CMS_Blog();
            s = s.GetBlogs();
            ViewBag.Type = "Edit";
            return View("Form", s);
        }

        [ValidateInput(false)]
        public ActionResult AddShow(HttpPostedFileBase main_image, HttpPostedFileBase sub_image, CMS_Blog s)
        {

            string directory = Server.MapPath("~/Content/images/TheMirror");
            string error_msg = "";

            if (main_image != null && main_image.ContentLength > 0)
            {
                List<string> Errors = Common.CheckImage(main_image, "main image", 0, 0);
                if (Errors.Count > 0)
                {
                    foreach (var item in Errors)
                    {
                        error_msg += item + " <br /> ";
                    }
                }
                else
                {
                    s.data.main_image = Common.SaveImage(main_image, directory);
                }

            }


            if (sub_image != null && sub_image.ContentLength > 0)
            {
                List<string> Errors = Common.CheckImage(sub_image, "sub image", 0, 0);
                if (Errors.Count > 0)
                {
                    foreach (var item in Errors)
                    {
                        error_msg += item + " <br /> ";
                    }
                }
                else
                {
                    s.data.sub_image = Common.SaveImage(sub_image, directory);
                }

            }

            if (!string.IsNullOrEmpty(error_msg))
            {
                ViewBag.Error = error_msg;
                ViewBag.Type = "ADD";
                var s2 = s.GetBlogs();      
                return View("Form", s);
            }

            s.AddBlog(s);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteShow(int delete_id)
        {
            CMS_Blog s = new CMS_Blog();
            s.DeleteBlog(delete_id);
            return RedirectToAction("Index");
        }

        [ValidateInput(false)]
        public ActionResult EditShow(int edit_id)
        {
            CMS_Blog s = new CMS_Blog();
            s = s.GetBlog(edit_id);
            ViewBag.Type = "EDIT2";
            return PartialView("Form", s);
        }

        [ValidateInput(false)]
        public ActionResult SaveEditShow(HttpPostedFileBase main_image, HttpPostedFileBase sub_image, CMS_Blog s)
        {
            string directory = Server.MapPath("~/Content/images/TheMirror");
            string error_msg = "";

            if (main_image != null && main_image.ContentLength > 0)
            {
                List<string> Errors = Common.CheckImage(main_image, "main image", 1073, 0);
                if (Errors.Count > 0)
                {
                    foreach (var item in Errors)
                    {
                        error_msg += item + " <br /> ";
                    }
                }
                else
                {
                    s.data.main_image = Common.SaveImage(main_image, directory);
                }

            }


            if (sub_image != null && sub_image.ContentLength > 0)
            {
                List<string> Errors = Common.CheckImage(sub_image, "sub image", 350, 250);
                if (Errors.Count > 0)
                {
                    foreach (var item in Errors)
                    {
                        error_msg += item + " <br /> ";
                    }
                }
                else
                {
                    s.data.sub_image = Common.SaveImage(sub_image, directory);
                }

            }

            if (!string.IsNullOrEmpty(error_msg))
            {
                ViewBag.Error = error_msg;
                ViewBag.Type = "ADD";
                return View("Form", s);
            }

            s.EditBlog(s);
            return RedirectToAction("Index");
        }
    }
}

