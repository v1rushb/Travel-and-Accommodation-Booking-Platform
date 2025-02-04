namespace TABP.Infrastructure.Extensions.Helpers;

public static class QueryableExtensions
{
    public static IQueryable<T> OrderByIf<T>(
        this IQueryable<T> source,
        bool condition,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
    {
        return condition
            ? orderBy(source)
            : source;
    }
}