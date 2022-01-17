using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Channel;

public class Channel : UserEntity
{
    public Guid WorkspaceId { get; protected set; }
    public string Name { get; protected set; }
    public bool IsPrivate { get; protected set; }

    public Workspace.Workspace Workspace { get; protected set; }
    public IEnumerable<ChannelUser> Users { get; protected set; } = new List<ChannelUser>();
}
