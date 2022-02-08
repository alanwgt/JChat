using AutoMapper;
using JChat.Application.Shared.Mappings;
using JChat.Domain.Entities.Notifications;
using JChat.Domain.Enums;

namespace JChat.Application.Notifications.Queries;

public class NotificationDto : IMapFrom<Notification>
{
    public Guid Id { get; set; }
    public Guid? WorkspaceId { get; set; }
    public string CreatedBy { get; set; }
    public string Meta { get; set; }
    public NotificationType Type { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Notification, NotificationDto>()
            .ForMember(
                d => d.CreatedBy,
                opt =>
                    opt.MapFrom(n => n.CreatedBy.Username)
            );
    }
}