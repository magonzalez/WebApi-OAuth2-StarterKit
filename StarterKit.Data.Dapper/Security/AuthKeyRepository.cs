using System;

using Dapper;

using StarterKit.Core.Security;
using StarterKit.Framework.Data;
using StarterKit.Framework.Data.Dapper;
using StarterKit.Framework.Logging;

namespace StarterKit.Data.Dapper.Security
{
    public class AuthKeyRepository : DataModelRepository<AuthKey>, IAuthKeyRepository
    {
        public AuthKeyRepository(IConnectionFactory connectionFactory, ILogger logger)
            : base(connectionFactory, logger)
        {
        }

        public bool ValidateAuthKey(Guid userId, Guid issuedGuid)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                var key = conn.Get<AuthKey>(new
                {
                    UserId = userId,
                    IssuedGuid = issuedGuid
                });

                return (key != null);
            }
        }

        public void SetAuthKey(Guid userId, Guid issuedGuid)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                var key = conn.Get<AuthKey>(new {UserId = userId});

                if (key == null)
                {
                    conn.Insert(new AuthKey
                    {
                        UserId = userId,
                        IssuedGuid = issuedGuid
                    });
                }
                else
                {
                    key.IssuedGuid = issuedGuid;
                    conn.Update(key);
                }
            }
        }
    }
}
