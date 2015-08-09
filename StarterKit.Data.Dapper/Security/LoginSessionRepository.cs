using System;
using System.Linq;

using Dapper;

using StarterKit.Core.Security;
using StarterKit.Framework.Data;
using StarterKit.Framework.Data.Dapper;
using StarterKit.Framework.Logging;

namespace StarterKit.Data.Dapper.Security
{
    public class LoginSessionRepository : DataModelRepository<LoginSession>, ILoginSessionRepository
    {
        public LoginSessionRepository(IConnectionFactory connectionFactory, ILogger logger)
            : base(connectionFactory, logger)
        {
        }

        public LoginSession GetCurrrentLogingSessionByUserId(Guid userId)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.GetList<LoginSession>(new {UserId = userId})
                    .OrderByDescending(s => s.LoginDateTime)
                    .FirstOrDefault();
            }
        }
    }
}
