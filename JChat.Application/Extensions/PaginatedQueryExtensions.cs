using JChat.Application.Shared.CQRS;

namespace JChat.Application.Extensions;

public static class PaginatedQueryExtensions
{
    public static PaginatedQuery<T> WithPaginationData<T>(this PaginatedQuery<T> query, PaginationData data)
    {
        query.PageNumber = data.PageNumber;
        query.PageSize = data.PageSize;

        return query;
    }
}