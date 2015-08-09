using StarterKit.Core.Users;
using StarterKit.Data.Dapper.Users;
using StructureMap.Configuration.DSL;

namespace StarterKit.WebApi.Ioc.Registries
{
    public class UsersRegistry : Registry
    {
        public UsersRegistry()
        {
            For<IUserRepository>().Use<UserRepository>();
        }
    }
}