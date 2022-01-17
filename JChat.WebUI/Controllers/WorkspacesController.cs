using JChat.Application.Shared.Models;
using JChat.Application.Workspaces.Commands;
using JChat.Application.Workspaces.Queries;
using JChat.WebUI.Controllers.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI.Controllers;

public class WorkspacesController : ApiController
{
    [HttpGet]
    public async Task<PaginatedList<WorkspaceBriefDto>> List([FromQuery] GetWorkspacesQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<WorkspaceBriefDto> Create([FromBody] CreateWorkspaceCommand command)
    {
        return await Mediator.Send(command);
    }
}