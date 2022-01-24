namespace JChat.Application.Shared.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException()
    {
    }

    public ForbiddenAccessException(string message) : base(message)
    {
    }
}
