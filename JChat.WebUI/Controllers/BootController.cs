using JChat.Application.Boot.Queries;
using JChat.WebUI.Controllers.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI.Controllers;

public class BootController : ApiController
{
    [HttpGet]
    public async Task<ActionResult<BootDto>> Init()
        => Ok(await Mediator.Send(new GetBootQuery()));
}