using AutoMapper;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Mappings;
using JChat.Application.Shared.Models;
using JChat.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Notifications.Queries;

public class GetNotificationsQuery : PaginatedQuery<NotificationDto>, IRequest<NotificationDto>, IHasUserSetter
{
    public IUser User { get; set; }
}

public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, PaginatedList<NotificationDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentWorkspaceService _currentWorkspace;

    public GetNotificationsQueryHandler(IApplicationDbContext context, IMapper mapper,
        ICurrentWorkspaceService currentWorkspace)
    {
        _context = context;
        _mapper = mapper;
        _currentWorkspace = currentWorkspace;
    }

    public Task<PaginatedList<NotificationDto>> Handle(GetNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Notifications
            .Where(n => n.UserId == request.User.Id);

        query = _currentWorkspace.WorkspaceId != null
            ? query.Where(n => n.WorkspaceId == _currentWorkspace.WorkspaceId || n.WorkspaceId == null)
            : query.Where(n => n.WorkspaceId == null);

        return query
            .AsNoTracking()
            .PaginatedListAsync(request, _mapper.ConfigurationProvider);
    }
}