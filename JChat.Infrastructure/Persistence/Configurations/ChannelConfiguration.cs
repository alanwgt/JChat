using JChat.Domain.Entities.Channel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JChat.Infrastructure.Persistence.Configurations;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .HasIndex(c => new { c.Name, c.WorkspaceId })
            .IsUnique();
    }
}
