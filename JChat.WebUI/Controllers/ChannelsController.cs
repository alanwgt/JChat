using JChat.Application.Channels.Commands;
using JChat.Application.Channels.Queries;
using JChat.Application.Extensions;
using JChat.Application.Messages.Commands;
using JChat.Application.Shared.CQRS;
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
    public async Task<ActionResult<PaginatedList<ChannelBriefDto>>> List([FromQuery] PaginationData data)
        => Ok(await Mediator.Send(new GetChannelsQuery().WithPaginationData(data)));

    [HttpGet("{ChannelId:guid}/users")]
    public async Task<ActionResult<PaginatedList<ChannelUserBriefDto>>> Users(Guid channelId,
        [FromQuery] PaginationData paginationData)
        => Ok(
            await Mediator.Send(new GetChannelUsersQuery { ChannelId = channelId }.WithPaginationData(paginationData)));

    [HttpPost("{ChannelId:guid}/messages")]
    public async Task<ActionResult<MessageBriefDto>> SendMessage(CreateMessageCommand command)
        => Ok(await Mediator.Send(command));
}