using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRM.Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "FindUser",
                url: "{controller}/{action}/{name}",
                defaults: new { controller = "User", action = "Find", name = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ShowUserListPage",
                url: "{controller}/{action}/{currentPage}",
                defaults: new { controller = "User", action = "ShowUserListPage", currentPage = UrlParameter.Optional }
            );
        }
    }
}
