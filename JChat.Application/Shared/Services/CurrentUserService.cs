using System.Security.Claims;
using JChat.Application.Shared.Interfaces;
using JChat.Domain.Entities.User;
using JChat.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace JChat.Application.Shared.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private IUser? _user;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IUser? User
    {
        get
        {
            if (_user != null)
                return _user;

            var ctx = _httpContextAccessor.HttpContext;

            if (ctx?.User == null)
                return null;

            var claims = ctx.User.Claims;
            var userIdString = claims.SingleOrDefault(c => c.Type == "UserId")?.Value;
            var username = ctx.User.Claims
                .SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var firstname = ctx.User.Claims
                .SingleOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var lastname = ctx.User.Claims
                .SingleOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;

            if (
                userIdString == null
                || username == null
                || firstname == null
                || lastname == null
                || !Guid.TryParse(userIdString, out var guid)
            )
            {
                return null;
            }

            _user = new User(
                guid,
                username,
                firstname,
                lastname
            );
            return _user;
        }
    }
}