using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

using Newtonsoft.Json.Serialization;

using StarterKit.Framework.Logging;
using StarterKit.WebApi.Handlers;

namespace StarterKit.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, ILogger logger)
        {
            InitializeAttributeRouting(config);
            InitializeMediaTypes(config);
            InitializeExceptionHandlers(config, logger);
        }

        private static void InitializeAttributeRouting(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void InitializeMediaTypes(HttpConfiguration config)
        {
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        private static void InitializeExceptionHandlers(HttpConfiguration config, ILogger logger)
        {
            // By default, we are initializing the WebApi HttpConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Default.
            // This means that during exception handling, the setting for <system.web><customErrors mode=””/></system.web>
            // will be followed to determine if exception details are returned to the caller or just some generic message.
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Default;

            config.Services.Replace(typeof(IExceptionHandler), new ApiExceptionHandler(logger));
            config.Services.Replace(typeof(IExceptionLogger), new ApiExceptionLogger(logger));
        }
    }
}
