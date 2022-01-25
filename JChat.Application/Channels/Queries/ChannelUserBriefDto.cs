using JChat.Application.Shared.Mappings;
using JChat.Domain.Entities.Channel;

namespace JChat.Application.Channels.Queries;

public class ChannelUserBriefDto : IMapFrom<ChannelUser>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ChannelId { get; set; }
}