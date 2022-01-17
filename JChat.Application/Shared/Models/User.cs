using JChat.Domain.Interfaces;

namespace JChat.Application.Shared.Models;

public record User : IUser
{
    public Guid Id { get; init; }
    public string Username { get; init; }
    public string Firstname { get; init; }
    public string Lastname { get; init; }
}