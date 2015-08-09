using StarterKit.WebApi.Ioc.Registries;
using StructureMap;

namespace StarterKit.WebApi.Ioc
{
    public static class IocBootstrapper
    {
        public static IContainer InitializeContainer(IContainer container = null)
        {
            container = container ?? new Container();

            container.Configure(x =>
            {
                x.AddRegistry<LoggingRegistry>();
            });

            return container;
        }
    }
}