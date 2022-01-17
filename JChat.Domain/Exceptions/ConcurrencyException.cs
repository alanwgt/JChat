namespace JChat.Domain.Exceptions;

public class ConcurrencyException : AggregateException
{
    public ConcurrencyException(string message) : base(message)
    {
    }
}
