using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Application.Sorting.ExpressionBuilders;

public static class BookingSortExpressionBuilder
{
    private static readonly Dictionary<string, Expression<Func<RoomBooking, object>>> SortExpressions =
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["TotalPrice"] = booking => booking.TotalPrice,
        ["CheckInDate"] = booking => booking.CheckInDate,
        ["CheckOutDate"] = booking => booking.CheckOutDate
    };

    private static readonly Dictionary<string, Expression<Func<RoomBooking, object>>> AdminSortExpressions =
        new(SortExpressions, StringComparer.OrdinalIgnoreCase)
    {
        ["CreationDate"] = booking => booking.CreationDate,
        ["ModificationDate"] = booking => booking.ModificationDate
    };
    public static Func<IQueryable<RoomBooking>, IOrderedQueryable<RoomBooking>> GetSortDelegate(
        BookingSortQuery sortQuery)
    {
        var sortExpressions = sortQuery.IsAdmin 
            ? AdminSortExpressions
            : SortExpressions;

        if (!sortExpressions.TryGetValue(sortQuery.SortBy, out var expression))
        {
            expression = roomBooking => roomBooking.TotalPrice;
        }

        return sortQuery.SortOrder?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true
            ? query => query.OrderByDescending(expression)
            : query => query.OrderBy(expression);
    }
}