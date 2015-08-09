using System.Web.Http;

using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using StructureMap;

using StarterKit.Framework.Logging;
using StarterKit.WebApi;
using StarterKit.WebApi.Ioc;
using StarterKit.WebApi.Security;

[assembly: OwinStartup(typeof(Startup))]

namespace StarterKit.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = IocBootstrapper.InitializeContainer(new Container());
            var logger = container.GetInstance<ILogger>();

            // Setup Authentication
            var authConfig = container.GetInstance<ApiOwinAuthConfig>();
            authConfig.ConfigureAuth(app);

            // Wire up IoC to the Web API Dependency Resolver
            var configuration = new HttpConfiguration
            {
                DependencyResolver = new StructureMapResolver(container)
            };

            WebApiConfig.Register(configuration, logger);
            authConfig.ConfigureHttpAuth(configuration);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(configuration);
        }
    }
}