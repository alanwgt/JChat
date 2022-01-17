using JChat.Application.Shared.Interfaces;
using JChat.Domain.Interfaces;

namespace JChat.Application.Shared.CQRS;

public record BaseRequest : IHasUserSetter
{
    public IUser User { get; set; }
}