using JChat.Application.Notifications.Queries;
using JChat.Application.Shared.Models;
using JChat.WebUI.Controllers.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI.Controllers;

public class NotificationsController : ApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<NotificationDto>>> List([FromQuery] GetNotificationsQuery query)
        => Ok(await Mediator.Send(query));
}