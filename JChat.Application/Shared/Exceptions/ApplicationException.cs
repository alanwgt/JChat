namespace JChat.Application.Shared.Exceptions;

public class ApplicationException : Exception
{
    public string? Details { get; set; }

    public ApplicationException()
    {
    }

    public ApplicationException(string? message) : base(message)
    {
    }

    public ApplicationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}