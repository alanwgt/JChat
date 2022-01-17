using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using JChat.Application.Shared.Interfaces;
using JChat.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace JChat.WebUI.Security;

public class KratosAuthenticationOptions : AuthenticationSchemeOptions
{
}

/// <summary>
/// @link https://github.com/ory/kratos/discussions/1072
/// </summary>
public class KratosAuthenticationHandler : AuthenticationHandler<KratosAuthenticationOptions>
{
    private const string SessionCookieName = "ory_kratos_session";
    private readonly IIdentityService _identityService;

    public KratosAuthenticationHandler(
        IOptionsMonitor<KratosAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IIdentityService identityService
    ) : base(options, logger, encoder, clock)
    {
        _identityService = identityService;
    }

    private AuthenticateResult ValidateToken(IUser user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Username),
            new (ClaimTypes.GivenName, user.Firstname),
            new (ClaimTypes.Surname, user.Lastname),
            new ("UserId", user.Id.ToString())
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new GenericPrincipal(identity, null);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // ORY Kratos can authenticate against an API through two different methods:
        // Cookie Authentication is for Browser Clients and sends a Session Cookie with each request.
        // Bearer Token Authentication is for Native Apps and other APIs and sends an Authentication header with each request.
        // We are validating both ways here by sending a /whoami request to ORY Kratos passing the provided authentication
        // methods on to Kratos.
        try
        {
            if (Request.Cookies.TryGetValue(SessionCookieName, out var cookie))
                return ValidateToken(await _identityService.GetUserByCookieAsync(SessionCookieName, cookie));

            if (Request.Headers.TryGetValue("Authorization", out var token))
                return ValidateToken(await _identityService.GetUserByTokenAsync(token));

            return AuthenticateResult.NoResult();
        }
        catch (Exception e)
        {
            return AuthenticateResult.Fail(e.Message);
        }
    }
}
