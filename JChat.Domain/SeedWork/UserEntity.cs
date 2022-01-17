using JChat.Domain.Entities.User;

namespace JChat.Domain.SeedWork;

public abstract class UserEntity : Entity
{
    public Guid UserId { get; init; }
    public User User { get; init; }
}
