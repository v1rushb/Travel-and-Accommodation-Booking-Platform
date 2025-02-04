using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel.Sort;

namespace TABP.Application.Sorting.ExpressionBuilders;

public static class HotelSortExpressionBuilder
{
    private static readonly Dictionary<string, Expression<Func<Hotel, object>>> SortExpressions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["Name"] = hotel => hotel.Name,
        ["StarRating"] = hotel => hotel.StarRating,
        ["City"] = hotel => hotel.City.Name
    };

    private static readonly Dictionary<string, Expression<Func<Hotel, object>>> AdminSortExpressions = 
        new(SortExpressions, StringComparer.OrdinalIgnoreCase)
    {
        ["CreationDate"] = hotel => hotel.CreationDate,
        ["ModificationDate"] = hotel => hotel.ModificationDate
    };

    public static Func<IQueryable<Hotel>, IOrderedQueryable<Hotel>> GetSortDelegate(
        HotelSortQuery sortQuery)
    {
        var sortExpressions = sortQuery.IsAdmin ? AdminSortExpressions : SortExpressions;

        if (!sortExpressions.TryGetValue(sortQuery.SortBy, out var expression))
        {
            expression = hotel => hotel.Name;
        }

        return sortQuery.SortOrder?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true
            ? query => query.OrderByDescending(expression)
            : query => query.OrderBy(expression);
    }
}
