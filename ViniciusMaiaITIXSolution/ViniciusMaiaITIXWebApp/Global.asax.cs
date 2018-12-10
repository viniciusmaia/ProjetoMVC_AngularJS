﻿using Newtonsoft.Json;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ViniciusMaiaITIXWebApp.App_Start;

namespace ViniciusMaiaITIXWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IocConfig.ConfigurarDependencias();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                DateParseHandling = DateParseHandling.None,
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };
        }
    }
}
