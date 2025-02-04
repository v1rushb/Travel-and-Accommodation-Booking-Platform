using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Cart.Sort;

namespace TABP.Application.Sorting.ExpressionBuilders;

public static class CartSortExpressionBuilder
{
    private static readonly Dictionary<string, Expression<Func<Cart, object>>> SortExpressions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["TotalPrice"] = cart => cart.TotalPrice,
        ["CheckOutDate"] = cart => cart.CheckOutDate,
        ["CreationDate"] = cart => cart.CreationDate,
    };

    private static readonly Dictionary<string, Expression<Func<Cart, object>>> AdminSortExpressions = 
        new(SortExpressions, StringComparer.OrdinalIgnoreCase)
    {
        ["ModificationDate"] = cart => cart.ModificationDate
    };

    public static Func<IQueryable<Cart>, IOrderedQueryable<Cart>> GetSortDelegate(
        CartSortQuery sortQuery)
    {
        var sortExpressions = sortQuery.IsAdmin ? AdminSortExpressions : SortExpressions;

        if (!sortExpressions.TryGetValue(sortQuery.SortBy, out var expression))
        {
            expression = cart => cart.TotalPrice;
        }

        return sortQuery.SortOrder?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true
            ? query => query.OrderByDescending(expression)
            : query => query.OrderBy(expression);
    }
}