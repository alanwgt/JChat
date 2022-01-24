namespace JChat.Application.Enums;

public enum AuthzNamespace
{
    Workspaces = 0,
    Channels = 1,
    Files = 2,
    Groups = 3,
}

public static class AuthzNamespaceExtensions
{
    public static string Str(this AuthzNamespace ns)
        => ns switch
        {
            AuthzNamespace.Workspaces => "workspaces",
            AuthzNamespace.Channels => "channels",
            AuthzNamespace.Files => "files",
            AuthzNamespace.Groups => "groups",
            _ => "unknown_ns"
        };
}