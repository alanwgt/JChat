using AutoMapper;
using JChat.Application.Shared.Dtos;
using JChat.Application.Shared.Mappings;
using JChat.Domain.Entities.Message;

namespace JChat.Application.Messages.Queries;

public class MessageProjectionDto : IMapFrom<MessageProjection>
{
    public Guid Id { get; set; }

    public Guid MessageId { get; set; }
    public Guid RecipientId { get; set; }
    public Guid SenderId { get; set; }
    public Guid ChannelId { get; set; }

    public string SenderName { get; set; }
    public bool IsInbound { get; set; }
    public string Body { get; set; }
    public string Meta { get; set; }
    public string Reactions { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? ReceivedAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime? ConfirmedVisualizationAt { get; set; }

    public MessageProjectionDto? ReplyingTo { get; }
    public UserBriefDto ForwardedBy { get; }

    public IdNameDto Priority { get; set; }
    public IdNameDto BodyType { get; set; }
}