namespace JChat.Application.Shared.CQRS;

public class ChannelScopedRequest<TRequest> : WorkspaceScopedRequest<TRequest>
{
    public Guid ChannelId { get; set; }
}