using JChat.Application.Shared.Models;
using JChat.Domain.Interfaces;

namespace JChat.Application.Shared.Interfaces;

public interface IIdentityService
{
    Task<bool> IsAuthorized(Guid userId, string resource);
    Task<IUser> GetUserAsync(Guid userId);
    Task<IUser> GetUserByTokenAsync(string token);
    Task<IUser> GetUserByCookieAsync(string name, string value);
    Task<Result> RegisterUserAsync(Guid id, string username, string firstname, string lastname);
}
