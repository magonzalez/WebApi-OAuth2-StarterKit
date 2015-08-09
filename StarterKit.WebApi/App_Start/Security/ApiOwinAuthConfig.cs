using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

using Owin;

using StarterKit.Core.Security;
using StarterKit.Core.Security.Crypto;
using StarterKit.Core.Users;

namespace StarterKit.WebApi.Security
{
    public class ApiOwinAuthConfig
    {
        private readonly ILoginSessionRepository _loginSessionRepository;
        private readonly IAuthKeyRepository _authKeyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;

        public ApiOwinAuthConfig(
            ILoginSessionRepository loginSessionRepository,
            IAuthKeyRepository authKeyRepository,
            IUserRepository userRepository,
            IHashingService hashingService)
        {
            _loginSessionRepository = loginSessionRepository;
            _authKeyRepository = authKeyRepository;
            _userRepository = userRepository;
            _hashingService = hashingService;
        }

        public static OAuthAuthorizationServerOptions OAuthAuthorizationOptions { get; private set; }
        public static OAuthBearerAuthenticationOptions OAuthAuthenticationOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the application for OAuth based flow
            PublicClientId = "MxRblSync.Api";
            OAuthAuthorizationOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Authenticate"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                Provider = new ApiAuthorizationProvider(
                    _loginSessionRepository,
                    _authKeyRepository,
                    _userRepository,
                    _hashingService,
                    PublicClientId),
#if DEBUG
                AllowInsecureHttp = true
#else
                AllowInsecureHttp = false
#endif
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthAuthorizationServer(OAuthAuthorizationOptions);

            OAuthAuthenticationOptions = new OAuthBearerAuthenticationOptions
            {
                Provider = new ApiAuthenticationProvider(_authKeyRepository)
            };

            app.UseOAuthBearerAuthentication(OAuthAuthenticationOptions);
        }

        public static AuthenticationTicket UnprotectToken(string token)
        {
            return OAuthAuthenticationOptions.AccessTokenFormat.Unprotect(token);
        }

        public void ConfigureHttpAuth(HttpConfiguration config)
        {
            config.Filters.Add(new AuthorizeAttribute());
        }
    }
}
