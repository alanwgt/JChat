using JChat.Domain.Entities.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JChat.Infrastructure.Persistence.Configurations;

public class MessagePriorityConfiguration : IEntityTypeConfiguration<MessagePriority>
{
    public void Configure(EntityTypeBuilder<MessagePriority> builder)
    {
        builder.Property(mp => mp.Priority)
            .HasColumnType("smallint");
    }
}