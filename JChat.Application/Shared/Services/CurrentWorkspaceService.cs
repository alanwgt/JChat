using JChat.Application.Shared.Interfaces;
using Microsoft.AspNetCore.Http;

namespace JChat.Application.Shared.Services;

public class CurrentWorkspaceService : ICurrentWorkspaceService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentWorkspaceService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? WorkspaceId
    {
        get
        {
            var request = _httpContextAccessor.HttpContext.Request;

            if (!request.Headers.TryGetValue("X-Workspace-Id", out var id))
                return null;

            if (Guid.TryParse(id, out var guid))
                return guid;

            return null;
        }
    }
}