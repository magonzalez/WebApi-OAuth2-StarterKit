using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

using StarterKit.Framework.Extensions;
using StarterKit.Core.Security;
using StarterKit.Core.Security.Crypto;
using StarterKit.Core.Users;

namespace StarterKit.WebApi.Security
{
    public class ApiAuthorizationProvider : OAuthAuthorizationServerProvider
    {
        private readonly ILoginSessionRepository _loginSessionRepository;
        private readonly IAuthKeyRepository _authKeyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;

        private readonly string _publicClientId;

        public ApiAuthorizationProvider(
            ILoginSessionRepository loginSessionRepository,
            IAuthKeyRepository authKeyRepository,
            IUserRepository userRepository,
            IHashingService hashingService,
            string publicClientId)
        {
            if (publicClientId == null)
                throw new ArgumentNullException("publicClientId");

            _loginSessionRepository = loginSessionRepository;
            _authKeyRepository = authKeyRepository;
            _userRepository = userRepository;
            _hashingService = hashingService;

            _publicClientId = publicClientId;
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var user = _userRepository.GetByUsername(context.UserName);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user and password combination doesn't exist");
                return ThreadingExtensions.NoResult;
            }

            if (user.Locked)
            {
                context.SetError("invalid_grant", "The user account is locked.");
                return ThreadingExtensions.NoResult;
            }

            if (!_hashingService.ValidateStringHash(context.Password, user.Password))
            {
                HandleFailedLogin(context, user);
                return ThreadingExtensions.NoResult;
            }

            // Build Claims Identity
            var identity = new ClaimsIdentity(ApiOwinAuthConfig.OAuthAuthorizationOptions.AuthenticationType);
            identity.BuildClaims(user);

            var issuedGuid = Guid.NewGuid();

            // Create the Properties
            var properties = CreateProperties(user);
            properties.SetIssuedGuid(issuedGuid);

            // Create the ticket and process it.
            var ticket = new AuthenticationTicket(identity, properties);
            context.Validated(ticket);

            SaveLoginInformation(user, issuedGuid);
            return ThreadingExtensions.NoResult;
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return ThreadingExtensions.NoResult;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return ThreadingExtensions.NoResult;
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                var expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return ThreadingExtensions.NoResult;
        }

        private static AuthenticationProperties CreateProperties(User user)
        {
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                { "username", user.Username },
                { "userId", user.Id.ToString() },
                { "userTypeId", ((int)user.UserType).ToString(CultureInfo.InvariantCulture) }
            });
        }

        private void SaveLoginInformation(User user, Guid issuedId)
        {
            _authKeyRepository.SetAuthKey(user.Id, issuedId);

            // Update User record with latest login information.
            user.LastLoginDate = DateTime.UtcNow;
            user.LoginAttempts = 0;
            user.Locked = false;
            _userRepository.Update(user);

            _loginSessionRepository.Insert(new LoginSession
            {
                UserId = user.Id,
                LoginDateTime = DateTime.UtcNow
            });
        }

        private void HandleFailedLogin(OAuthGrantResourceOwnerCredentialsContext context, User user)
        {
            user.LoginAttempts = (byte)(user.LoginAttempts + 1);
            if (user.LoginAttempts > 3)
            {
                user.Locked = true;
            }

            _userRepository.Update(user);
            
            var errorMessage = (user.LoginAttempts < 3)
                ? string.Format("The password is incorrect, you have {0} attempts left. ", 3 - user.LoginAttempts)
                : "The user account is locked.";

            context.SetError("invalid_grant", errorMessage);
        }
    }
}