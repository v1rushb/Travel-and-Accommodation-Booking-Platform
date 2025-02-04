using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.City.Sort;

namespace TABP.Application.Sorting.ExpressionBuilders;

public static class CitySortExpressionBuilder
{
    private static readonly Dictionary<string, Expression<Func<City, object>>> SortExpressions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["Name"] = city => city.Name,
        ["CountryName"] = city => city.CountryName
    };

    private static readonly Dictionary<string, Expression<Func<City, object>>> AdminSortExpressions = 
        new(SortExpressions, StringComparer.OrdinalIgnoreCase)
    {
        ["CreationDate"] = city => city.CreationDate,
        ["ModificationDate"] = city => city.ModificationDate
    };

    public static Func<IQueryable<City>, IOrderedQueryable<City>> GetSortDelegate(
        CitySortQuery sortQuery)
    {
        var sortExpressions = sortQuery.IsAdmin ? AdminSortExpressions : SortExpressions;

        if (!sortExpressions.TryGetValue(sortQuery.SortBy, out var expression))
        {
            expression = city => city.Name;
        }

        return sortQuery.SortOrder?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true
            ? query => query.OrderByDescending(expression)
            : query => query.OrderBy(expression);
    }
}