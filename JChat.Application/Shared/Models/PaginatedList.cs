using JChat.Application.Shared.CQRS;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Shared.Models;

public class PaginatedList<T>
{
    public IReadOnlyList<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items.AsReadOnly();
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<PaginatedList<TList>> CreateAsync<TList>(IQueryable<TList> source, int pageNumber,
        int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<TList>(items, count, pageNumber, pageSize);
    }

    public static Task<PaginatedList<TList>> CreateAsync<TList>(IQueryable<TList> source, PaginatedQuery<TList> query)
        => CreateAsync(source, query.PageNumber, query.PageSize);
}