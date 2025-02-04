using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview.Sort;

namespace TABP.Application.Sorting.ExpressionBuilders;

public static class ReviewSortExpressionBuilder
{
    private static readonly Dictionary<string, Expression<Func<HotelReview, object>>> SortExpressions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["Rating"] = review => review.Rating,
        ["CreationDate"] = review => review.CreationDate,
    };

    private static readonly Dictionary<string, Expression<Func<HotelReview, object>>> AdminSortExpressions = 
        new(SortExpressions, StringComparer.OrdinalIgnoreCase)
    {
        ["ModificationDate"] = review => review.ModificationDate
    };

    public static Func<IQueryable<HotelReview>, IOrderedQueryable<HotelReview>> GetSortDelegate(
        ReviewSortQuery sortQuery)

    {
        var sortExpressions = sortQuery.IsAdmin ? AdminSortExpressions : SortExpressions;

        if (!sortExpressions.TryGetValue(sortQuery.SortBy, out var expression))
        {
            expression = review => review.Rating;
        }

        return sortQuery.SortOrder?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true
            ? query => query.OrderByDescending(expression)
            : query => query.OrderBy(expression);
    }
}