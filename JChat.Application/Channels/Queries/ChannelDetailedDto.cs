using JChat.Application.Shared.Dtos;
using JChat.Domain.Entities.Message;

namespace JChat.Application.Channels.Queries;

public class ChannelDetailedDto
{
    public ChannelBriefDto Channel { get; set; }
    public IEnumerable<UserBriefDto> Members { get; set; }
    // public IEnumerable<Message> Messages { get; set; }
    public IEnumerable<MessageProjection> Messages { get; set; }
    // public IEnumerable<MessageRecipient> MessageRecipients { get; set; }
}