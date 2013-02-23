using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DataUploadClient
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DataView", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DataUpload",
                url: "{controller}/{action}",
                defaults: new
                {
                    controller = "DataUpload",
                    action = "Index",
                    category = UrlParameter.Optional
                }
                );

            routes.MapRoute(
            name: "GenealogyUpload",
            url: "{controller}/{action}",
            defaults: new
            {
                controller = "Genealogy",
                action = "UploadIndex",
                category = UrlParameter.Optional
            }
            );

            routes.MapRoute(
                name: "Genealogy",
                url: "{controller}/{action}",
                defaults: new
                {
                    controller = "Genealogy",
                    action = "Index",
                    category = UrlParameter.Optional
                }
                );

         
        }
    }
}