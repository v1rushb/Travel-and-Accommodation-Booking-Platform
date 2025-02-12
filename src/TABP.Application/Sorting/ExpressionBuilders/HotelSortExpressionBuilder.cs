using System.Linq.Expressions;
using TABP.Domain.Models.Hotel.Sort;
using TABP.Domain.Models.Hotels;

namespace TABP.Application.Sorting.ExpressionBuilders;

public static class HotelSortExpressionBuilder
{
    private static readonly Dictionary<string, Expression<Func<HotelInsightDTO, object>>> SortExpressions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["Name"] = hotel => hotel.Name,
        ["StarRating"] = hotel => hotel.StarRating,
        ["City"] = hotel => hotel.CityName
    };

    private static readonly Dictionary<string, Expression<Func<HotelInsightDTO, object>>> AdminSortExpressions = 
        new(SortExpressions, StringComparer.OrdinalIgnoreCase)
    {
        ["CreationDate"] = hotel => hotel.CreationDate,
        ["ModificationDate"] = hotel => hotel.ModificationDate,
        ["Revenue"] = hotel => hotel.Revenue
    };

    public static Func<IQueryable<HotelInsightDTO>, IOrderedQueryable<HotelInsightDTO>> GetSortDelegate(
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
