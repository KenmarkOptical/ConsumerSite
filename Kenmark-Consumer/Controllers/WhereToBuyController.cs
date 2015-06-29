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
            return View(w);
        }

        public ActionResult GetZip()
        {
            MaxMindGeo m = new MaxMindGeo();
            string zip = m.UserLocation().Postal.Code;
            return Json(new { zip = zip }, JsonRequestBehavior.AllowGet);
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

    }
}
