

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
            TestCMS cm = new TestCMS()
            {
                Carousel_Photos = new List<TestCarouselPhoto>(){
                       new TestCarouselPhoto(){ Name = "IMAGE 1", Order = 1},
                       new TestCarouselPhoto(){ Name = "IMAGE 2", Order = 2},
                       new TestCarouselPhoto(){ Name = "IMAGE 3", Order = 3},
                       new TestCarouselPhoto(){ Name = "IMAGE 4", Order = 4},
                       new TestCarouselPhoto(){ Name = "IMAGE 5", Order = 5},
                       new TestCarouselPhoto(){ Name = "IMAGE 6", Order = 6},
                       new TestCarouselPhoto(){ Name = "IMAGE 7", Order = 7},
                       new TestCarouselPhoto(){ Name = "IMAGE 8", Order = 8}
                  }
            };

            return View("Index", cm);
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

        [HttpPost]
        public ActionResult UpdateHome(TestCMS home)
        {

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateOurStory(TestCMS ourstory)
        {

            return RedirectToAction("Index");
        }

    }


}
