using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ChatApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ChatApi.DataServices.Connection.Constr = System.Configuration.ConfigurationManager.ConnectionStrings["ChatApp"].ToString();
            ChatApi.DataServices.Connection.DataBaseName = (new DataServices.CommonDB()).con.Database;

           
        }
    }
}
