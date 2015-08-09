using System;
using System.Collections.Generic;
using System.Linq;

using Dapper;

using StarterKit.Framework.Data;
using StarterKit.Core.Users;
using StarterKit.Framework.Data.Dapper;
using StarterKit.Framework.Logging;

namespace StarterKit.Data.Dapper.Users
{
    public class UserRepository : DataModelRepository<User>, IUserRepository
    {
        public UserRepository(IConnectionFactory connectionFactory, ILogger logger)
            : base(connectionFactory, logger)
        {
        }

        public IEnumerable<string> GetMatchingUsernames(string username)
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string username)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.GetList<User>(new {UserName = username})
                    .FirstOrDefault();
            }
        }

        public UserType GetUserType(Guid userId)
        {
            var user = Get(userId);
            if (user != null)
            {
                return user.UserType;
            }

            return UserType.Unknown;
        }
    }
}
