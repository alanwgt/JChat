using Microsoft.AspNetCore.SignalR;

namespace JChat.Application.Notifications;

public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        // TODO: get workspace id
        await base.OnConnectedAsync();
        await Groups.AddToGroupAsync(Context.ConnectionId, "");
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

    public Task SendChannelMessage(Guid channelId, string message)
    {
        return Clients.Group(channelId.ToString()).SendAsync(message);
    }

    public Task SendWorkspaceMessage(string message)
    {
        return Clients.Group("").SendAsync("", message);
    }
}
