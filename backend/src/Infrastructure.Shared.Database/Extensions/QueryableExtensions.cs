using Infrastructure.Shared.Database.Models;

namespace Infrastructure.Shared.Database.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        return await PagedList<T>.CreateAsync(query, pageNumber, pageSize, cancellationToken);
    }

    public static async Task<PagedList<TDestination>> ToPagedListAsync<TSource, TDestination>(
        this IQueryable<TSource> query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        where TDestination : class
    {
        return await PagedList<TDestination>.CreateAsync(query, pageNumber, pageSize, cancellationToken);
    }
}