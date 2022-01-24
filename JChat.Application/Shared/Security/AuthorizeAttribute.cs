namespace JChat.Application.Shared.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizeAttribute : Attribute
{
    public AuthorizeAttribute() { }

    public string? Subject { get; set; } = null;
    public string Object { get; set; }
    public string Namespace { get; set; }
    public string Relation { get; set; }
}
