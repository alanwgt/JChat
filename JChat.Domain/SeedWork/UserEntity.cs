using JChat.Domain.Entities.User;

namespace JChat.Domain.SeedWork;

public abstract class UserEntity : Entity
{
    public Guid UserId { get; protected set; }
    public User User { get; protected set; }
}
