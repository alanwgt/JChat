using JChat.Domain.Entities.Channel;
using JChat.Domain.Interfaces;
using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.User;

public class User : Entity, IUser
{
    public string Username { get; init; }
    public string Firstname { get; init; }
    public string Lastname { get; init; }

    public ICollection<ChannelUser> Channels { get; } = new List<ChannelUser>();
    public ICollection<UserWorkspace> UserWorkspaces { get; } = new List<UserWorkspace>();

    private User()
    {
    }

    public User(Guid guid, string username, string firstName, string lastName)
    {
        Id = guid;
        Username = username;
        Firstname = firstName;
        Lastname = lastName;
    }
}
