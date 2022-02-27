using JChat.Application.Enums;
using JChat.Application.Shared.Exceptions;
using JChat.Application.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ApplicationException = JChat.Application.Shared.Exceptions.ApplicationException;
using IAuthorizationService = JChat.Application.Shared.Interfaces.IAuthorizationService;

namespace JChat.Application.Notifications;

[Authorize]
public class ChatHub : Hub<IClientHub>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUserService _currentUserService;

    public ChatHub(ICurrentUserService currentUserService, IAuthorizationService authorizationService)
    {
        _currentUserService = currentUserService;
        _authorizationService = authorizationService;
    }

    protected IClientHub GetClientHub(Guid userId)
        => Clients.Client(OnlineUserManager.GetConnectionString(userId));

    public bool IsUserOnline(Guid userId)
    {
        if (!OnlineUserManager.HasUser(userId))
            return false;

        var connectionString = OnlineUserManager.GetConnectionString(userId);
        return Clients.Client(connectionString) != null;
    }

    public override async Task OnConnectedAsync()
    {
        if (_currentUserService.User == null)
            throw new ApplicationException { Details = "exceptions.hub.unknown_user" };

        await base.OnConnectedAsync();

        OnlineUserManager.AddUser(_currentUserService.User.Id, Context.ConnectionId, _currentUserService.User);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);

        if (_currentUserService.User == null || !OnlineUserManager.HasUser(_currentUserService.User.Id))
            return;

        var (connectionId, _, groups) = OnlineUserManager.GetData(_currentUserService.User.Id);

        foreach (var group in groups)
        {
            await Groups.RemoveFromGroupAsync(connectionId, group);
            await Clients.OthersInGroup(group).UserDisconnected(_currentUserService.User.Id);
        }

        OnlineUserManager.RemoveUser(_currentUserService.User.Id);
    }

    public async Task ConnectToWorkspace(string workspaceId)
    {
        var userId = _currentUserService.User.Id;
        var authzRequest = _authorizationService.Can(
            AuthzNamespace.Workspaces.Str(),
            workspaceId,
            AuthzRelation.Read.Str(),
            userId.ToString(),
            new CancellationToken()
        );
        if (!await authzRequest)
        {
            throw new ForbiddenAccessException($"current user has no access to workspace {workspaceId}");
        }

        OnlineUserManager.AddGroupToUser(userId, workspaceId);
        await Groups.AddToGroupAsync(Context.ConnectionId, workspaceId);
        await Clients.Caller.ConnectedToWorkspace(Guid.Parse(workspaceId));
        await Clients.Caller.SetOnlineUsers(OnlineUserManager.GetOnlineUsers(workspaceId));
        await Clients.OthersInGroup(workspaceId).UserConnected(userId);
    }

    public async Task DisconnectFromWorkspace(string workspaceId)
    {
        if (_currentUserService.User == null || !OnlineUserManager.HasUser(_currentUserService.User.Id))
            return;

        OnlineUserManager.RemoveGroupFromUser(_currentUserService.User.Id, workspaceId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, workspaceId);
        await Clients.Caller.DisconnectedFromWorkspace(Guid.Parse(workspaceId));
        await Clients.OthersInGroup(workspaceId).UserDisconnected(_currentUserService.User.Id);
    }

    public Task ExecuteIfOnline(Guid userId, Func<IClientHub, Task> fn)
        => !IsUserOnline(userId)
            ? Task.CompletedTask
            : fn(GetClientHub(userId));
}