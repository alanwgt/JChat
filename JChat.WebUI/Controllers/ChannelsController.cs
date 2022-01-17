using JChat.Application.Channels.Commands;
using JChat.WebUI.Controllers.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI.Controllers;

public class ChannelController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateChannelCommand command)
        => Ok(await Mediator.Send(command));
}
