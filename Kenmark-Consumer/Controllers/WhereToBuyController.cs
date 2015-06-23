using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Kenmark_Consumer.Controllers
{
    public class WhereToBuyController : Controller
    {
        //
        // GET: /WhereToBuy/

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetCustomers(WhereToBuy data)
        {
            data = data.GetCustomers(data);

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var resultData = data.Customers;
            var result = new ContentResult
            {
                Content = serializer.Serialize(resultData),
                ContentType = "application/json"
            };

            return Json(new { Model = data, GooglePoints = result }, JsonRequestBehavior.AllowGet); 
        }

    }
}
