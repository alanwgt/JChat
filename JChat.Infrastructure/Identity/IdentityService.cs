using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Models;
using JChat.Domain.Interfaces;
using JChat.Infrastructure.Exceptions;
using JChat.Infrastructure.Extensions;
using JChat.Infrastructure.Interfaces;
using JChat.Infrastructure.Persistence;
using Ory.Kratos.Client.Api;
using User = JChat.Domain.Entities.User.User;

namespace JChat.Infrastructure.Identity;

public class IdentityService : IIdentityService
{

    private readonly V0alpha2Api _kratosInstance;
    private readonly ApplicationDbContext _dbContext;

    public IdentityService(IInfrastructureConfig infrastructureConfig, ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        var kratosConfig = new Ory.Kratos.Client.Client.Configuration
        {
            BasePath = infrastructureConfig.KratosBasePath,
        };

        _kratosInstance = new V0alpha2Api(kratosConfig);
    }

    public Task<bool> IsAuthorized(Guid userId, string resource)
    {
        throw new NotImplementedException();
    }

    public async Task<IUser> GetUserAsync(Guid userId)
    {
        return await _dbContext.Users.FindAsync(userId)
               ?? throw new UserNotFoundException("");
    }

    public Task<IUser> GetUserByTokenAsync(string token)
    {
        var session = _kratosInstance.ToSession(token);
        return Task.FromResult(session.Identity.ToApplicationUser());
    }

    public Task<IUser> GetUserByCookieAsync(string name, string value)
    {
        var session = _kratosInstance.ToSession(null, $"{name}={value}");
        return Task.FromResult(session.Identity.ToApplicationUser());
    }

    public async Task<Result> RegisterUserAsync(Guid id, string username, string firstname, string lastname)
    {
        var user = new User(id, username, firstname, lastname);
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }
}
