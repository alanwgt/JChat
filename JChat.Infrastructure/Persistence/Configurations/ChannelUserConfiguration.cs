using JChat.Domain.Entities.Channel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JChat.Infrastructure.Persistence.Configurations;

public class ChannelUserConfiguration : IEntityTypeConfiguration<ChannelUser>
{
    public void Configure(EntityTypeBuilder<ChannelUser> builder)
    {
        builder
            .HasIndex(cu => new { cu.ChannelId, cu.UserId })
            .IsUnique();
    }
}