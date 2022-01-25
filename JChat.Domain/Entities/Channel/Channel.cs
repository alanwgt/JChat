using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Channel;

public class Channel : AuditableEntity
{
    public Guid WorkspaceId { get; protected set; }
    public string Name { get; protected set; }
    public bool IsPrivate { get; protected set; }

    public Workspace.Workspace Workspace { get; protected set; }
    public ICollection<ChannelUser> Users { get; protected set; } = new List<ChannelUser>();

    protected Channel()
    {
    }

    public Channel(Guid workspaceId, string name, bool isPrivate)
    {
        WorkspaceId = workspaceId;
        Name = name;
        IsPrivate = isPrivate;
    }

    public ChannelUser AddUser(Guid userId, bool isAdmin = false)
    {
        var cUser = new ChannelUser(Id, userId, isAdmin);
        Users.Add(cUser);
        return cUser;
    }
}
