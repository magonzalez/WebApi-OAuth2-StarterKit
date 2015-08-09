using StarterKit.Core.Security;
using StarterKit.Core.Security.Crypto;
using StarterKit.Data.Dapper.Security;
using StructureMap.Configuration.DSL;

namespace StarterKit.WebApi.Ioc.Registries
{
    public class SecurityRegistry : Registry
    {
        public SecurityRegistry()
        {
            For<ILoginSessionRepository>().Use<LoginSessionRepository>();
            For<IAuthKeyRepository>().Use<AuthKeyRepository>();
            For<IHashingService>().Use<HashingService>();
        }
    }
}