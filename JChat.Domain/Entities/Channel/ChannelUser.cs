using System.ComponentModel.DataAnnotations.Schema;
using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Channel;

public class ChannelUser : Entity
{
    public Guid ChannelId { get; protected set; }
    public Guid UserId { get; protected set; }
    public bool IsAdmin { get; protected set; }

    [ForeignKey("ChannelId")] public Channel Channel { get; }
    [ForeignKey("UserId")] public User.User User { get; }

    protected ChannelUser()
    {
    }

    public ChannelUser(Guid channelId, Guid userId, bool admin = false)
    {
        ChannelId = channelId;
        UserId = userId;
        IsAdmin = admin;
    }

    public void SetIsAdmin(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }
}
