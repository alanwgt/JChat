using JChat.Domain.Entities.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JChat.Infrastructure.Persistence.Configurations;

public class MessageProjectionConfiguration : IEntityTypeConfiguration<MessageProjection>
{
    public void Configure(EntityTypeBuilder<MessageProjection> builder)
    {
        builder.Property(mp => mp.Reactions)
            .HasColumnType("jsonb");

        builder.HasIndex(mp => new { mp.ChannelId, mp.RecipientId });
    }
}