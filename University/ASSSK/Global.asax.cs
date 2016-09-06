using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//these using statements for the logging
using ASSSK.DataAccesLayer;
using System.Data.Entity.Infrastructure.Interception;

namespace ASSSK
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //these too the logging
            DbInterception.Add(new UniversityInterceptorTransientErrors());
            DbInterception.Add(new UniversityInterceptorLogging());
        }
    }
}
