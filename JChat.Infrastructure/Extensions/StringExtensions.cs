namespace JChat.Infrastructure.Extensions;

public static class StringExtensions
{
    public static Guid? ToGuid(this string? guid)
        => guid == null ? null : Guid.Parse(guid);
}
