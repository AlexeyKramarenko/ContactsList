using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;
using System.Web.Mvc;

namespace ContactsList
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

          
            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "CompanyList",
                url: "{controller}/{action}/{maximumRows}/{startRowIndex}/{sortByExpression}",
                defaults: new { controller = "Default", action = "GetNextCompanyList" });
             
            routes.MapRoute(
                name: "SortedCompanyList",
                url: "{controller}/{action}/{rowCount}/{sortByExpression}",
                defaults: new { controller = "Default", action = "GetSortedCompanyList" });

            //webforms
            routes.MapPageRoute("Admin", routeUrl: "admin/companies", physicalFile: "~/Admin/Companies.aspx");
            routes.MapPageRoute(null, routeUrl: "admin/add_company", physicalFile: "~/Admin/AddCompany.aspx");
            routes.MapPageRoute(null, routeUrl: "admin/edit_countries", physicalFile: "~/Admin/EditCountries.aspx");
            routes.MapPageRoute(null, routeUrl: "admin/edit_towns", physicalFile: "~/Admin/EditTowns.aspx");

        }
    }
}
