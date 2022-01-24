namespace JChat.Application.Enums;

public enum AuthzRelation
{
    Ownership = 10,
    Manage = 20,
    Member = 30,
    Write = 40,
    Read = 50,
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