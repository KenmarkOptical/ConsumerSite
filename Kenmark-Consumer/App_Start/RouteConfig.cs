﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kenmark_Consumer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Penguin",
                url: "Original-Penguin-Eyewear",
                defaults: new { controller = "Collection", action = "Index", id = "Penguin" }
            );


            routes.MapRoute(
                name: "ViewCollection",
                url: "Eyewear/{id}",
                defaults: new { controller = "Collection", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}