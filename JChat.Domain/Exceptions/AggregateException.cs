namespace JChat.Domain.Exceptions;

public class AggregateException : DomainException
{
    public AggregateException(string message) : base(message)
    {
    }
}
