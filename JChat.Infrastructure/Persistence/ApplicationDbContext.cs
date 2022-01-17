using System.Reflection;
using JChat.Application.Shared.Interfaces;
using JChat.Domain.Entities.Channel;
using JChat.Domain.Entities.Message;
using JChat.Domain.Entities.User;
using JChat.Domain.Entities.Workspace;
using JChat.Domain.Interfaces;
using JChat.Domain.SeedWork;
using JChat.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using IDomainEvent = JChat.Domain.Interfaces.IDomainEvent;

namespace JChat.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDomainEventService _domainEventService;
    private readonly IDateTime _dateTime;

    public DbSet<User> Users => Set<User>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<MessageHighlight> MessageHighlights => Set<MessageHighlight>();
    public DbSet<MessagePriority> MessagePriorities => Set<MessagePriority>();
    public DbSet<MessageReaction> MessageReactions => Set<MessageReaction>();
    public DbSet<MessageRecipient> MessageRecipients => Set<MessageRecipient>();
    public DbSet<MessageType> MessageTypes => Set<MessageType>();
    public DbSet<Reaction> Reactions => Set<Reaction>();
    public DbSet<ChannelUser> ChannelUsers => Set<ChannelUser>();
    public DbSet<Workspace> Workspaces => Set<Workspace>();
    public DbSet<UserWorkspace> UserWorkspaces => Set<UserWorkspace>();

    private ApplicationDbContext()
    {
    }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDomainEventService domainEventService,
        IDateTime dateTime,
        ICurrentUserService currentUserService)
        : base(options)
    {
        _domainEventService = domainEventService;
        _dateTime = dateTime;
        _currentUserService = currentUserService;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = _dateTime.Now;
                    break;

                case EntityState.Deleted:
                    entry.Entity.DeletedAt = _dateTime.Now;
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = _dateTime.Now;
                    entry.Entity.CreatedById = _currentUserService.User?.Id;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = _dateTime.Now;
                    entry.Entity.LastModifiedById = _currentUserService.User?.Id;
                    break;

                case EntityState.Deleted:
                    entry.Entity.DeletedAt = _dateTime.Now;
                    entry.Entity.DeletedById = _currentUserService.User?.Id;
                    break;
            }
        }

        var events = ChangeTracker.Entries<IAggregateRoot>()
            .Select(x => x.Entity.GetUncommittedEvents())
            .SelectMany(x => x)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToArray();

        // TODO: add try on unique key conflict code: 23505
        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyAuditableEntityConfiguration();
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    private async Task DispatchEvents(IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}