using JChat.Application.Shared.Dtos;
using Ory.Keto.Client.Model;

namespace JChat.Application.Boot.Queries;

public class BootDto
{
    public IEnumerable<IdNameDto> MessagePriorities { get; set; }
    public IEnumerable<IdNameDto> MessageReactions { get; set; }
    public IEnumerable<IdNameDto> MessageTypes { get; set; }
    public IEnumerable<KetoInternalRelationTuple> Permissions { get; set; }
}