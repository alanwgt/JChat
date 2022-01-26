using JChat.Application.Shared.Models;
using JChat.Application.Workspaces.Commands;
using JChat.Application.Workspaces.Queries;
using JChat.WebUI.Controllers.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI.Controllers;

public class WorkspacesController : ApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<WorkspaceBriefDto>>> List([FromQuery] GetWorkspacesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<ActionResult<WorkspaceBriefDto>> Create([FromBody] CreateWorkspaceCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("invite")]
    public async Task<IActionResult> Invite(InviteToWorkspaceCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPost("accept-invite")]
    public async Task<ActionResult<WorkspaceBriefDto>> AcceptInvite(AcceptWorkspaceInviteCommand command)
        => Ok(await Mediator.Send(command));

    [HttpPost("banish")]
    public async Task<IActionResult> Banish(BanishUserFromWorkspaceCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPost("reject-invite")]
    public async Task<IActionResult> RejectInvite(RejectWorkspaceInviteCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}