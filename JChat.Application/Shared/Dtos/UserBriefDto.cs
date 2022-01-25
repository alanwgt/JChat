using AutoMapper;
using JChat.Application.Shared.Mappings;
using JChat.Domain.Entities.User;

namespace JChat.Application.Shared.Dtos;

public class UserBriefDto : IMapFrom<User>
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserBriefDto>()
            .ForMember(d => d.Name, opt => opt.MapFrom(u =>
                string.Join(" ", u.Firstname, u.Lastname))
            );
    }
}