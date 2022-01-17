using JChat.Domain.Interfaces;

namespace JChat.Application.Shared.Interfaces;

public interface IHasUserSetter
{
    public IUser User { get; set; }
}