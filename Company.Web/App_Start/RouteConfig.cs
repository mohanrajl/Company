using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Company.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AboutFolder",
                url: "About/{action}/{id}",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ServicesFolder",
                url: "Services/{action}/{id}",
                defaults: new { controller = "Services", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NewsFolder",
                url: "News/{action}/{id}",
                defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ContactFolder",
                url: "Contact/{action}/{id}",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UserFolder",
                url: "User/{action}/{id}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
