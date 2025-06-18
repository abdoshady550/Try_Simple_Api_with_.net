using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace Asp.net_Web_Api.Authentications
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Console.WriteLine("Headers: " + string.Join(", ", Request.Headers.Select(h => $"{h.Key}: {h.Value}")));
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.NoResult());


            var authHeader = Request.Headers["Authorization"].ToString();
            if (!authHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(AuthenticateResult.Fail("Unknown Scheme"));
            var encoded = authHeader["Basic".Length..];

            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));

            var userNameAndPassword = decoded.Split(":");
            if (userNameAndPassword[0] != "admin" || userNameAndPassword[1] != "password")
                return Task.FromResult(AuthenticateResult.Fail("Wrong username or password"));


            var claims = new[] { new Claim(ClaimTypes.Name, userNameAndPassword[0]) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));


        }
    }
}
