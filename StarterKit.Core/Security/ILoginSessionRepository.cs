using System;
using StarterKit.Framework.Data;

namespace StarterKit.Core.Security
{
    public interface ILoginSessionRepository : IDataModelRepository<LoginSession>
    {
        LoginSession GetCurrrentLogingSessionByUserId(Guid userId);
    }
}
