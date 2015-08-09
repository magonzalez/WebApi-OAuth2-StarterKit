using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using StarterKit.Framework.Logging;

namespace StarterKit.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly ILogger _logger;

        public WebApiApplication(ILogger logger)
            : this()
        {
            _logger = logger;
        }

        public WebApiApplication()
        {

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(config =>
            {
                WebApiConfig.Register(config, _logger);
            });

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
