using JChat.Application.Shared.Dtos;
using JChat.Application.Shared.Models;
using JChat.Application.Users.Queries;
using JChat.WebUI.Controllers.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI.Controllers;

public class UsersController : ApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<UserBriefDto>>> List([FromQuery] GetUsersQuery query)
        => Ok(await Mediator.Send(query));
}