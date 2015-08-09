using StructureMap.Configuration.DSL;

using StarterKit.Framework.Logging;
using StarterKit.Framework.Logging.Log4Net;

namespace StarterKit.WebApi.Ioc.Registries
{
    public class LoggingRegistry : Registry
    {
        public LoggingRegistry()
        {
            // Initial loging
            Log4NetLogger.Configure();

            For<ILogger>().Use<Log4NetLogger>();
        }

    }
}