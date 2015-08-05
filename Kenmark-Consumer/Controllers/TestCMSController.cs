

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
               

               Image image = Image.FromStream(photo.InputStream);

               var fileName = Path.GetFileName(photo.FileName);

               var imageDimension = image.PhysicalDimension;
               var imageHeight = image.Height;
               var imageWidth = image.Width;
               
              
               photo.SaveAs(Path.Combine(directory, fileName));

              
              
            }


            return RedirectToAction("Index");
        }

    }
}
