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

        public ActionResult GetZipGEO(string latitude = "", string longitude = "")
        {
            string zip = "";            

            if (HttpContext.Request.Cookies["geo_loc_zip"] == null)
            {
                if (!String.IsNullOrEmpty(latitude) && !String.IsNullOrEmpty(longitude))
                {
                    MaxMindGeo m = new MaxMindGeo();
                    zip = m.GetZipFromLatLong(latitude, longitude);             
                }
                else
                {
                    MaxMindGeo m = new MaxMindGeo();
                    zip = m.UserLocation().Postal.Code;
                }

                //store the cookie value if not empty
                if (!String.IsNullOrEmpty(zip))
                {
                    HttpCookie cookie = new HttpCookie("geo_loc_zip");
                    cookie.Value = zip;
                    HttpContext.Response.SetCookie(cookie);
                }
            }
            else
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("geo_loc_zip");
                zip = cookie.Value;
            }


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

            if (HttpContext.Request.Cookies["geo_loc_zip"] == null)
            {              
                //store the cookie value
                HttpCookie cookie = new HttpCookie("geo_loc_zip");
                cookie.Value = data.Zip;
                HttpContext.Response.SetCookie(cookie);
            }
            else
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("geo_loc_zip");
                HttpContext.Response.Cookies.Remove("geo_loc_zip");
                cookie.Value = data.Zip;
                HttpContext.Response.SetCookie(cookie);
            }


            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var resultData = data.Customers;


            string html = RenderPartialViewToString("_Grid", data);
            return Json(new { html = html, GooglePoints = serializer.Serialize(resultData)}, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult Email(String email, String zip, int radius) {
            return null;
        }

    }
}
