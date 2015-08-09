using System;
using System.Security.Claims;
using System.Security.Principal;

namespace StarterKit.Core.Security
{
    public static class IdentityExtensions
    {
        public static Guid GetUserGuid(this IIdentity identity)
        {
            var userId = identity.GetUserId();

            return userId != null
                ? Guid.Parse(userId)
                : Guid.Empty;
        }

        public static string GetUserId(this IIdentity identity)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");

            var ci = identity as ClaimsIdentity;
            if (ci == null)
                return null;

            var claim = ci.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null 
                ? claim.Value
                : null;
        }
    }
}
