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
}