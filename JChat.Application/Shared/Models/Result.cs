namespace JChat.Application.Shared.Models;

public class Result
{
    public bool Succeeded { get; }
    public string[] Errors { get; }

    internal Result(bool succeeded, IEnumerable<string> errorMessages)
    {
        Succeeded = succeeded;
        Errors = errorMessages.ToArray();
    }

    public static Result Success()
        => new(true, Array.Empty<string>());

    public static Result Failure(IEnumerable<string> errorMessages)
        => new(false, errorMessages);
}
