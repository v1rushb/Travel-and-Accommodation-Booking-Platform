using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel.Sort;

namespace TABP.Application.Sorting.ExpressionBuilders;

public static class HotelSortExpressionBuilder
{
    private static readonly Dictionary<string, Expression<Func<Hotel, object>>> SortExpressions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["name"] = hotel => hotel.Name,
        ["starRating"] = hotel => hotel.StarRating,
        ["city"] = hotel => hotel.City.Name
    };

    private static readonly Dictionary<string, Expression<Func<Hotel, object>>> AdminSortExpressions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["name"] = hotel => hotel.Name,
        ["starRating"] = hotel => hotel.StarRating,
        ["city"] = hotel => hotel.City.Name,
        ["creationDate"] = hotel => hotel.CreationDate,
        ["modificationDate"] = hotel => hotel.ModificationDate
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
