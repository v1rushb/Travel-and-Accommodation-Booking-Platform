using System.Linq.Expressions;
using TABP.Domain.Models.Room.Sort;

namespace TABP.Application.Sorting.ExpressionBuilders;

public static class RoomSortExpressionBuilder
{
    private static readonly Dictionary<string, Expression<Func<AvailableRoom, object>>> SortExpressions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["Number"] = room => room.Number,
        ["PricePerNight"] = room => room.PricePerNight,
        ["AdultsCapacity"] = room => room.AdultsCapacity,
        ["ChildrenCapacity"] = room => room.ChildrenCapacity
    };

    private static readonly Dictionary<string, Expression<Func<AvailableRoom, object>>> AdminSortExpressions = 
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["CreationDate"] = room => room.CreationDate,
        ["ModificationDate"] = room => room.ModificationDate
    };

    public static Func<IQueryable<AvailableRoom>, IOrderedQueryable<AvailableRoom>> GetSortDelegate(
        RoomSortQuery sortQuery)
    {
        var sortExpressions = sortQuery.IsAdmin ? AdminSortExpressions : SortExpressions;

        if (!sortExpressions.TryGetValue(sortQuery.SortBy, out var expression))
        {
            expression = room => room.Number;
        }

        return sortQuery.SortOrder?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true
            ? query => query.OrderByDescending(expression)
            : query => query.OrderBy(expression);
    }
}