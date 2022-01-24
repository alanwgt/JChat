namespace JChat.Application.Shared.Interfaces;

public interface IHasWorkspaceIdSetter
{
    public Guid WorkspaceId { set; get; }
}