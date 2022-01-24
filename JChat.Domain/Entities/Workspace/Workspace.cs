using JChat.Domain.Entities.User;
using JChat.Domain.Enums;
using JChat.Domain.Exceptions;
using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Workspace;

public class Workspace : AuditableEntity
{
    public string Name { get; private set; }

    public ICollection<UserWorkspace> UserWorkspaces { get; } = new List<UserWorkspace>();
    public ICollection<Channel.Channel> Channels { get; } = new List<Channel.Channel>();

    public Workspace(string name)
    {
        if (name.Length < 3) throw new DomainValidationException(ValidationExceptionType.MinLength, nameof(name), 3);

        Name = name;
    }

    public UserWorkspace AddMember(Guid userId, bool admin = false)
    {
        var userWorkspace = new UserWorkspace(Id, userId, admin);
        UserWorkspaces.Add(userWorkspace);
        return userWorkspace;
    }

    public Channel.Channel CreateChannel(string name, bool isPrivate)
    {
        var channel = new Channel.Channel(Id, name, isPrivate);
        Channels.Add(channel);
        return channel;
    }
}
