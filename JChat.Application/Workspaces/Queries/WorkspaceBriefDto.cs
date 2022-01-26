using JChat.Application.Shared.Mappings;
using JChat.Domain.Entities.Workspace;

namespace JChat.Application.Workspaces.Queries;

public class WorkspaceBriefDto : IMapFrom<Workspace>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Joined { get; set; }
}