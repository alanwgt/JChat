using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Extensions;

public static class DbSetExtensions
{
    public static ValueTask<T?> FindAsync_<T>(this DbSet<T> set, Guid entityId,
        CancellationToken cancellationToken = new())
        where T : class
        => set.FindAsync(new object?[] { entityId }, cancellationToken);
}