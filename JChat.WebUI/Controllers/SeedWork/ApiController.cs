using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI.Controllers.SeedWork;

[Authorize]
[ApiController]
[Route("[controller]")]
public abstract class ApiController : ControllerBase
{
    private ISender _mediator = null;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
