using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class HookController : ControllerBase
{
    private readonly ILogger<HookController> _logger;
    private readonly IApplicationDbContext _applicationDbContext;

    public HookController(IApplicationDbContext applicationDbContext, ILogger<HookController> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    [HttpPost("kratos/registration")]
    public async Task<IActionResult> KratosRegistrationHook([FromBody] User kratosUser,
        CancellationToken cancellationToken)
    {
        var user = new Domain.Entities.User.User(kratosUser.Id, kratosUser.Username, kratosUser.Firstname,
            kratosUser.Lastname);
        _logger.LogInformation("creating user. Kratos: {kratosUser} Model: {user}", kratosUser, user);

        await _applicationDbContext.Users.AddAsync(user, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}