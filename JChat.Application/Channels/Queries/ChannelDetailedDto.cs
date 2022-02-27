using JChat.Application.Messages.Queries;
using JChat.Application.Shared.Dtos;

namespace JChat.Application.Channels.Queries;

public class ChannelDetailedDto
{
    public ChannelBriefDto Channel { get; set; }
    public IEnumerable<UserBriefDto> Members { get; set; }
    // public IEnumerable<Message> Messages { get; set; }
    public IEnumerable<MessageProjectionDto> Messages { get; set; }
    // public IEnumerable<MessageRecipient> MessageRecipients { get; set; }
}