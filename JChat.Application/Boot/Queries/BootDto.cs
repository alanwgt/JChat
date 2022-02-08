using JChat.Application.Channels.Queries;
using JChat.Application.Shared.Dtos;

namespace JChat.Application.Boot.Queries;

public class BootDto
{
    public IEnumerable<IdNameDto> MessagePriorities { get; set; }
    public IEnumerable<IdNameDto> MessageReactions { get; set; }
    public IEnumerable<IdNameDto> MessageTypes { get; set; }
    public IEnumerable<ChannelBriefDto> Channels { get; set; }
    public IEnumerable<UserBriefDto> Users { get; set; }
    public UserBriefDto Me { get; set; }
    public IEnumerable<object> Permissions { get; set; }
}