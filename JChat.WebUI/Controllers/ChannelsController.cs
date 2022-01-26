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

    [HttpGet("{channelId:guid}/users")]
    public async Task<ActionResult<PaginatedList<ChannelUserBriefDto>>> Users(Guid channelId,
        [FromQuery] PaginationData paginationData)
        => Ok(
            await Mediator.Send(new GetChannelUsersQuery { ChannelId = channelId }.WithPaginationData(paginationData)));

    [HttpPost("{channelId:guid}/users")]
    public async Task<ActionResult<ChannelUserBriefDto>> AddMember(AddUserToChannelCommand command, Guid channelId)
    {
        command.ChannelId = channelId;
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("{channelId:guid}/users/{userId:guid}/admin")]
    public async Task<ActionResult<ChannelUserBriefDto>> SetAdmin(ChangeUserChannelAdmCommand command, Guid channelId,
        Guid userId)
    {
        command.ChannelId = channelId;
        command.UserId = userId;
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("{channelId:guid}/messages")]
    public async Task<ActionResult<MessageBriefDto>> SendMessage(CreateMessageCommand command, Guid channelId)
    {
        command.ChannelId = channelId;
        return Ok(await Mediator.Send(command));
    }
}