using Microsoft.EntityFrameworkCore;

namespace TABP.Infrastructure.Extensions.Helpers;

public static class PaginationExtension
{
    public static async Task<List<T>> PaginateAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        var Items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Items;
    }
    public static IEnumerable<T> PaginateAsync<T>(
        this IEnumerable<T> query,
        int pageNumber,
        int pageSize)
    {
        var Items = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Items;
    }

    public static IQueryable<T> Paginate<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}