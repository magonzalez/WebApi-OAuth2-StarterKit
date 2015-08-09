using System.Data;
using System.Data.SqlClient;

using StarterKit.Framework.Data;

namespace StarterKit.Data.Dapper
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public ConnectionFactory(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public IDbConnection GetConnection(string csName = null)
        {
            var conn = new SqlConnection(_connectionStringProvider.GetConnectionString(csName));
            conn.Open();

            return conn;
        }
    }
}
