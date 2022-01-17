using System.Text.Json.Serialization;

namespace JChat.Infrastructure.Identity.Models;

public class KratosUser
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("active")]
    public bool Active { get; init; }

    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; init; }

    [JsonPropertyName("authenticated_at")]
    public DateTime AuthenticatedAt { get; init; }

    [JsonPropertyName("issued_at")]
    public DateTime IssuedAt { get; init; }

    [JsonPropertyName("identity")]
    public Identity Identity { get; init; }
}

public class Identity
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
    [JsonPropertyName("traits")]
    public Traits Traits { get; init; }
}

public class Traits
{
    [JsonPropertyName("username")]
    public string Username { get; init; }
    [JsonPropertyName("name")]
    public Name Name { get; init; }
}

public class Name
{
    [JsonPropertyName("first")]
    public string First { get; init; }
    [JsonPropertyName("last")]
    public string Last { get; init; }
}
