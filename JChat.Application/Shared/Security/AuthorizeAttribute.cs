namespace JChat.Application.Shared.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizeAttribute : Attribute
{
    public AuthorizeAttribute() { }

    public string Subject { get; set; } = string.Empty;
    public string Object { get; set; } = string.Empty;
    public string Namespace { get; set; } = string.Empty;
}
