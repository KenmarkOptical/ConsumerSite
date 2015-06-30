using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Kenmark_Consumer.Controllers
{
    public class WhereToBuyController : MyBaseController
    {
        //
        // GET: /WhereToBuy/

        public ActionResult Index()
        {         
            WhereToBuy w = new WhereToBuy();
            w.Radius = 25;
            return View(w);
        }

        public ActionResult GetZipGEO()
        {
            MaxMindGeo m = new MaxMindGeo();
            string zip = m.UserLocation().Postal.Code;
            WhereToBuy data = new WhereToBuy();
            data.Zip = zip;
            data.Radius = 25;
            
            data = data.GetCustomers(data);

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var resultData = data.Customers;
            string html = RenderPartialViewToString("_Grid", data);
            return Json(new { zip = zip, html = html, GooglePoints = serializer.Serialize(resultData) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomers(WhereToBuy data)
        {
            data = data.GetCustomers(data);

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var resultData = data.Customers;
            //var result = new ContentResult
            //{
            //    Content = serializer.Serialize(resultData),
            //    ContentType = "application/json"
            //};

            string html = RenderPartialViewToString("_Grid", data);
            return Json(new { html = html, GooglePoints = serializer.Serialize(resultData)}, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult Email(String email, String zip, int range) {
            return null;
        }

    }
}
