namespace JChat.Domain.Exceptions;

public class DomainException : Exception
{
    public string? Details { get; set; }

    public DomainException()
    {
    }

    public DomainException(string message) : base(message)
    {
    }
}