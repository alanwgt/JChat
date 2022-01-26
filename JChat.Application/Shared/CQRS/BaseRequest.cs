using JChat.Application.Shared.Interfaces;
using JChat.Domain.Interfaces;
using MediatR;

namespace JChat.Application.Shared.CQRS;

public class BaseRequest<TRequest> : IHasUserSetter, IRequest<TRequest>
{
    public IUser User { get; set; }
}