﻿using EmployeeManagement.WebUI.DI;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EmployeeManagement.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CastleWindsor.Setup();
            AutoMapperConfiguration.Configure();


        }

    protected void Application_End()
        {
            CastleWindsor.Dispose();
        }
    }
}