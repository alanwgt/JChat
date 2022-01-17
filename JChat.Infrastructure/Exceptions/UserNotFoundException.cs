namespace JChat.Infrastructure.Exceptions;

public class UserNotFoundException : InfrastructureException
{
    public UserNotFoundException(string message) : base(message)
    {
    }
}
