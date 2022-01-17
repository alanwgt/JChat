using JChat.Domain.Entities.Workspace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JChat.Infrastructure.Persistence.Configurations;

public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.Property(w => w.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(w => w.Name)
            .IsUnique();

        builder
            .HasMany(w => w.UserWorkspaces)
            .WithOne(uw => uw.Workspace)
            .HasForeignKey(w => w.WorkspaceId);
    }
}
