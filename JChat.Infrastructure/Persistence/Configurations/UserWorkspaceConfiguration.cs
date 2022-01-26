using JChat.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JChat.Infrastructure.Persistence.Configurations;

public class UserWorkspaceConfiguration : IEntityTypeConfiguration<UserWorkspace>
{
    public void Configure(EntityTypeBuilder<UserWorkspace> builder)
    {
        builder
            .HasOne(uw => uw.User)
            .WithMany(u => u.UserWorkspaces)
            .HasForeignKey(uw => uw.UserId);

        builder
            .HasOne(uw => uw.Workspace)
            .WithMany(w => w.UserWorkspaces)
            .HasForeignKey(uw => uw.WorkspaceId);

        builder
            .HasIndex(uw => new { uw.UserId, uw.WorkspaceId })
            .IsUnique();

        builder.HasQueryFilter(uw => uw.BanishedById == null);
    }
}