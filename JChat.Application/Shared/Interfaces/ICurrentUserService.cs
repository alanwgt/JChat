using JChat.Domain.Interfaces;

namespace JChat.Application.Shared.Interfaces;

public interface ICurrentUserService
{
    IUser? User { get; }
}