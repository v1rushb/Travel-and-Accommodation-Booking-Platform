using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Discount.Sort;

namespace TABP.Application.Sorting.ExpressionBuilders;

public static class DiscountSortExpressionBuilder
{
    private static readonly Dictionary<string, Expression<Func<Discount, object>>> SortExpressions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["AmountPercentage"] = discount => discount.AmountPercentage,
        ["StartingDate"] = discount => discount.StartingDate,
        ["EndingDate"] = discount => discount.EndingDate,

    };

    private static readonly Dictionary<string, Expression<Func<Discount, object>>> AdminSortExpressions = 
        new(SortExpressions, StringComparer.OrdinalIgnoreCase)
    {
        ["ModificationDate"] = discount => discount.ModificationDate,
        ["CreationDate"] = discount => discount.CreationDate,
    };

    public static Func<IQueryable<Discount>, IOrderedQueryable<Discount>> GetSortDelegate(
        DiscountSortQuery sortQuery)
    {
        var sortExpressions = sortQuery.IsAdmin 
            ? AdminSortExpressions
            : SortExpressions;

        if (!sortExpressions.TryGetValue(sortQuery.SortBy, out var expression))
        {
            expression = discount => discount.AmountPercentage;
        }

        return sortQuery.SortOrder?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true
            ? query => query.OrderByDescending(expression)
            : query => query.OrderBy(expression);
    }
}