using System.Configuration;

namespace StarterKit.Framework.Data
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string GetConnectionString(string name = null)
        {
            name = name ?? "DefaultConnection";

            var cs = ConfigurationManager.ConnectionStrings[name];
            if (cs != null)
                return cs.ConnectionString;

            return null;
        }
    }
}
