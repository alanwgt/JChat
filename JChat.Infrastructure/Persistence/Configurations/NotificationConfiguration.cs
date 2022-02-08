using JChat.Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JChat.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(n => n.Type)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(n => n.Meta)
            .HasColumnType("jsonb")
            .HasDefaultValue("{}");
    }
}