using System;
using StarterKit.Framework.Data;

namespace StarterKit.Core.Users
{
    public interface IUserRepository : IDataModelRepository<User>
    {
        User GetByUsername(string username);
        UserType GetUserType(Guid userId);
    }
}
