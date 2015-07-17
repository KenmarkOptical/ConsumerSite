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
                zip = m.UserLocation().Postal.Code;

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
            return View(s);
        }

    }
}
