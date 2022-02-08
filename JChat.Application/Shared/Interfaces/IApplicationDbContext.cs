using JChat.Domain.Entities.Channel;
using JChat.Domain.Entities.Message;
using JChat.Domain.Entities.Notifications;
using JChat.Domain.Entities.User;
using JChat.Domain.Entities.Workspace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace JChat.Application.Shared.Interfaces;

public interface IApplicationDbContext
{
   Task<int> SaveChangesAsync(CancellationToken cancellationToken);
   DbSet<Workspace> Workspaces { get; }
   DbSet<User> Users { get; }
   DbSet<Message> Messages { get; }
   DbSet<MessageHighlight> MessageHighlights { get; }
   DbSet<MessagePriority> MessagePriorities { get; }
   DbSet<MessageReaction> MessageReactions { get; }
   DbSet<MessageRecipient> MessageRecipients { get; }
   DbSet<MessageBodyType> MessageBodyTypes { get; }
   DbSet<MessageProjection> MessageProjections { get; }
   DbSet<Reaction> Reactions { get; }
   DbSet<ChannelUser> ChannelUsers { get; }
   DbSet<UserWorkspace> UserWorkspaces { get; }
   DbSet<Channel> Channels { get; }
   DbSet<Notification> Notifications { get; }
   DatabaseFacade Database { get; }
}
