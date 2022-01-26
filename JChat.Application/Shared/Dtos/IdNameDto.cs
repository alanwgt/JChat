using JChat.Application.Shared.Mappings;
using JChat.Domain.Entities.Message;

namespace JChat.Application.Shared.Dtos;

public class IdNameDto : IMapFrom<MessagePriority>, IMapFrom<MessageType>, IMapFrom<Reaction>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}