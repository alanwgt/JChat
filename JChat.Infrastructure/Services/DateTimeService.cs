using JChat.Application.Shared.Interfaces;

namespace JChat.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now.ToUniversalTime();
}