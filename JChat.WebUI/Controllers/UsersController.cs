using JChat.Application.Shared.Dtos;
using JChat.Application.Shared.Models;
using JChat.Application.Users.Queries;
using JChat.WebUI.Controllers.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI.Controllers;

public class UsersController : ApiController
{
    // [HttpGet]
    // public async Task<ActionResult<PaginatedList<UserBriefDto>>> FromWorkspace([FromQuery] GetWorkspaceUsersQuery query)
    //     => Ok(await Mediator.Send(query));
    [HttpGet("search")]
    public async Task<ActionResult<PaginatedList<UserBriefDto>>> Query([FromQuery] GetUsersQuery query)
        => Ok(await Mediator.Send(query));
}