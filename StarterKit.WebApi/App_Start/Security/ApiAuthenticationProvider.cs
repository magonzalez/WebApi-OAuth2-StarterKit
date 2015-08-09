using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using StarterKit.Framework.Extensions;
using StarterKit.Core.Security;

namespace StarterKit.WebApi.Security
{
    public class ApiAuthenticationProvider : OAuthBearerAuthenticationProvider
    {
        private readonly IAuthKeyRepository _authKeyRepository;

        public ApiAuthenticationProvider(IAuthKeyRepository authKeyRepository)
        {
            _authKeyRepository = authKeyRepository;
        }

        public override Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            if (context.Ticket.Properties.ExpiresUtc < DateTime.UtcNow)
            {
                context.SetError("invalid_grant", "Access Token has expired.");
                context.Rejected();
                return ThreadingExtensions.NoResult;
            }

            var userId = context.Ticket.Identity.GetUserGuid();
            var issuedGuid = context.Ticket.Properties
                .GetIssuedGuid();

            if (!_authKeyRepository.ValidateAuthKey(userId, issuedGuid))
            {
                context.SetError("invalid_token", "Access Token has not been properly set or has been invalidated.");
                context.Rejected();
                return ThreadingExtensions.NoResult;
            }

            context.Validated();
            return ThreadingExtensions.NoResult;
        }

        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            var tokenCookie = context.OwinContext.Request.Cookies["BearerToken"];
            if (!string.IsNullOrEmpty(tokenCookie))
            {
                context.Token = tokenCookie;
                return Task.FromResult<object>(null);
            }

            return base.RequestToken(context);
        }
    }
}