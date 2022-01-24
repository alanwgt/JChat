using JChat.Application.Shared.Mappings;
using JChat.Domain.Entities.Channel;

namespace JChat.Application.Channels.Queries;

public class ChannelBriefDto : IMapFrom<Channel>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}