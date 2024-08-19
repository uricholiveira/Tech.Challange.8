using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Shared.Database.Models;

public class PagedList<T>
{
    private PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        Pagination = new PaginationModel(count, pageNumber, pageSize, (int)Math.Ceiling(count / (double)pageSize));
        Data = new List<T>(items);
    }

    public PaginationModel Pagination { get; private set; }
    public List<T> Data { get; private set; }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var count = await source.CountAsync(cancellationToken);
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static async Task<PagedList<T>> CreateAsync<TSource>(IQueryable<TSource> source, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var count = await source.CountAsync(cancellationToken);
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectToType<T>()
            .ToListAsync(cancellationToken);
        var response = new PagedList<T>(items, count, pageNumber, pageSize);
        return response;
    }
}