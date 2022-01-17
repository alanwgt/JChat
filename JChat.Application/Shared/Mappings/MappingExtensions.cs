using AutoMapper;
using AutoMapper.QueryableExtensions;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Models;
using JChat.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Shared.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : Entity =>
        PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable,
        IConfigurationProvider configuration)
        => queryable.ProjectTo<TDestination>(configuration).ToListAsync();

    // public static async Task<PaginatedList<TProjection>> PaginatedListAsync<TSource, TProjection>(
    //     this IQueryable<TSource> queryable, PaginatedQuery<TProjection> query,
    //     IConfigurationProvider configuration)
    //     => queryable.ProjectTo<TProjection>(configuration).PaginatedListAsync(query.PageNumber, query.PageSize);
}