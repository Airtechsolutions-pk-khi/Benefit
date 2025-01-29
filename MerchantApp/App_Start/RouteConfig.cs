using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MerchantApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    //                url: "{controller}/{action}/{id}",

            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Direct_Transfer", id = UrlParameter.Optional }
            //    );
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home",action = "Benefit", id = UrlParameter.Optional }
            
            );
        }
    }
}
