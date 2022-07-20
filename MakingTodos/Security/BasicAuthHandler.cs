using MakingTodos.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace MakingTodos.Security
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder enconder,
            ISystemClock clock,
            IUserService userService
            ) : base(options, logger, enconder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("There is no header");

            bool result = false;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var email = credentials[0];
                var password = credentials[1];
                result = _userService.IsUser(email, password);
            }
            catch
            {
                return AuthenticateResult.Fail("Something wrong happen");
            }

            if(!result)
                return AuthenticateResult.Fail("Wrong password or username");

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "id"),
                new Claim(ClaimTypes.Name, "user"),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);

        }
    }
}
