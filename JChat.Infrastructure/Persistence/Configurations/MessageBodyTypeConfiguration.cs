using JChat.Domain.Entities.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JChat.Infrastructure.Persistence.Configurations;

public class MessageBodyTypeConfiguration : IEntityTypeConfiguration<MessageBodyType>
{
    public void Configure(EntityTypeBuilder<MessageBodyType> builder)
    {
        builder.Property(mbt => mbt.BodyType)
            .HasColumnType("smallint");
    }
}