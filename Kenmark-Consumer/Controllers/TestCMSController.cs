

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
    public class TestCMSController : Controller
    {
        //
        // GET: /TestCMS/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateContent(TestCMS t)
        {
          
            
            return View();
        }


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase photo)
        {
            string directory = Server.MapPath("~/Content/images/TheMirror");
            

            if (photo != null && photo.ContentLength > 0)
            {

                
                var fileName = Path.GetFileName(photo.FileName);
               
                photo.SaveAs(Path.Combine(directory, fileName));

                //Image image = Image.FromFile("~/Content/images/TheMirror/" + fileName);

            }


            return RedirectToAction("Index");
        }

    }
}
