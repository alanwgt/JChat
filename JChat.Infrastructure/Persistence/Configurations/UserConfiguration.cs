using JChat.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JChat.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(u => u.UserWorkspaces)
            .WithOne(uw => uw.User)
            .HasForeignKey(uw => uw.UserId);
    }
}