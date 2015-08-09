using System;
using StarterKit.Framework.Data;

namespace StarterKit.Core.Security
{
    public interface IAuthKeyRepository : IDataModelRepository<AuthKey>
    {
        bool ValidateAuthKey(Guid userId, Guid issuedGuid);
        void SetAuthKey(Guid userId, Guid issuedGuid);
    }
}
