using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class StyleController : Controller
    {
        //
        // GET: /Style/

        public ActionResult Index(string sku)
        {
            
            string zip = "";
            if (HttpContext.Request.Cookies["geo_loc_zip"] == null)
            {
                MaxMindGeo m = new MaxMindGeo();
               // zip = m.UserLocation().Postal.Code;
                zip = "40299";

                //store the cookie value
                HttpCookie cookie = new HttpCookie("geo_loc_zip");
                cookie.Value = zip;
                HttpContext.Response.SetCookie(cookie);
            }
            else
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("geo_loc_zip");
                zip = cookie.Value;
            }


           
            Style s = new Style().GetStyle(sku);
            s.customers = s.customers.GetCustomers(new WhereToBuy() { Radius = 90, Zip = zip }, 4);
            ViewBag.Zip = zip;
            ViewBag.Description = "Click here to go directly to this frames page!";
            ViewBag.Image = "http://kenmark.kenmarkoptical.com/showimage.aspx?img=" + s.Main_Color + "&w=650";
            ViewBag.Title = HttpUtility.HtmlEncode(s.Style_Name);
            ViewBag.URL = "http://1181.kenmarkoptical.com/Style?sku=" + s.SKU;
            
            return View(s);
        }

        public ActionResult ChangeZip(string zip)
        {           
            if (HttpContext.Request.Cookies["geo_loc_zip"] == null)
            {
                //store the cookie value
                HttpCookie cookie = new HttpCookie("geo_loc_zip");
                cookie.Value = zip;
                HttpContext.Response.SetCookie(cookie);
            }
            else
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("geo_loc_zip");
                cookie.Value = zip;
                HttpContext.Response.SetCookie(cookie);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}
