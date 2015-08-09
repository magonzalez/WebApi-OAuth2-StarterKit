using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Owin.Security;

using StarterKit.Core.Security;
using StarterKit.Core.Users;

namespace StarterKit.WebApi.Security
{
    public static class ApiOAuthProviderHelper
    {
        public const string IssuedGuidProperty = "Mx-IssuedGuid";

        public static void BuildClaims(this ClaimsIdentity identity, User user)
        {
            identity.AddClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(CustomClaimTypes.UserType, user.UserType.ToString())
            });
        }

        public static Guid GetIssuedGuid(this AuthenticationProperties properties)
        {
            return Guid.Parse(properties.Dictionary[IssuedGuidProperty]);
        }

        public static void SetIssuedGuid(this AuthenticationProperties properties, Guid issuedGuid)
        {
            properties.Dictionary[IssuedGuidProperty] = issuedGuid.ToString();
        }
    }
}