using JChat.Domain.Entities.Channel;
using JChat.Domain.Entities.Message;
using JChat.Domain.Entities.User;
using JChat.Domain.Entities.Workspace;
using Microsoft.EntityFrameworkCore;

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
   DbSet<MessageType> MessageTypes { get; }
   DbSet<Reaction> Reactions { get; }
   DbSet<ChannelUser> ChannelUsers { get; }
   DbSet<UserWorkspace> UserWorkspaces { get; }
}
