using JChat.Application.Channels.Commands;
using JChat.Application.Channels.Queries;
using JChat.Application.Shared.Models;
using JChat.WebUI.Controllers.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI.Controllers;

public class ChannelsController : ApiController
{
    [HttpPost]
    public async Task<ActionResult<ChannelBriefDto>> Create(CreateChannelCommand command)
        => Ok(await Mediator.Send(command));

    [HttpGet]
    public async Task<ActionResult<PaginatedList<ChannelBriefDto>>> List([FromQuery] GetChannelsQuery query)
        => Ok(await Mediator.Send(query));

    [HttpGet("{ChannelId}/users")]
    public async Task<ActionResult<PaginatedList<ChannelUserBriefDto>>> ListUsers(GetChannelUsersQuery query)
        => Ok(await Mediator.Send(query));
}
