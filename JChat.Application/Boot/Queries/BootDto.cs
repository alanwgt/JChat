using JChat.Application.Shared.Dtos;

namespace JChat.Application.Boot.Queries;

public class BootDto
{
    public IEnumerable<IdNameDto> MessagePriorities { get; set; }
    public IEnumerable<IdNameDto> MessageReactions { get; set; }
    public IEnumerable<IdNameDto> MessageTypes { get; set; }
}