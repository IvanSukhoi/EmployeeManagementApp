using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace EmployeeManagement.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "Admin/{action}/{id}",
                new
                {
                    controller = "Admin",
                    action = "EditDeveloper",
                    id = UrlParameter.Optional
                });


            routes.MapRoute(null, "Admin/Edit/{id}",
                new
                {
                    controller = "Admin",
                    action = "EditManager",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Employee",
                    action = "List",
                    category = (string)null,
                    page = 1
                }
            );

            routes.MapRoute(null,
                "Page{page}",
                new { controller = "Employee", action = "List", category = (string)null },
                new { page = @"\d+" }
            );


            routes.MapRoute(null,
                "{category}",
                new { controller = "Employee", action = "List", page = 1 }
            );

            routes.MapRoute(null,
                "{category}/Page{page}",
                new { controller = "Employee", action = "List" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
