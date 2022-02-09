namespace JChat.Application.Enums;

public enum AuthzRelation
{
    Ownership = 1000,
    Manage = 800,
    Member = 600,
    Write = 400,
    Read = 200,
}

public static class AuthzRelationExtensions
{
    public static string Str(this AuthzRelation relation)
        => relation switch
        {
            AuthzRelation.Ownership => "ownership",
            AuthzRelation.Manage => "manage",
            AuthzRelation.Member => "member",
            AuthzRelation.Write => "write",
            AuthzRelation.Read => "read",
            _ => "unknown"
        };
}