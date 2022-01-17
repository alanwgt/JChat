using JChat.Application;
using JChat.Application.Shared.Security;
using JChat.Domain.Events;
using JChat.WebUI.Controllers.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI.Controllers;

[Authorize]
public class TestController : ApiController
{
    private readonly ILogger<TestController> _logger;
    private readonly IDomainEventService _eventService;

    public TestController(ILogger<TestController> logger, IDomainEventService eventService)
    {
        _logger = logger;
        _eventService = eventService;
    }

    [HttpGet("evt")]
    public async Task<IActionResult> TestMe()
    {
        _logger.LogInformation("Raising event {Event}", nameof(TestEvent));
        await _eventService.Publish(new TestEvent());
        return Ok(new { Controller = nameof(TestController), Func = nameof(TestMe) });
    }

    [HttpGet("cmd")]
    public async Task<IActionResult> Cmd([FromQuery] TestCommand cmd)
    {
        _logger.LogInformation("Sending test command");
        return Ok(await Mediator.Send(cmd));
    }
}