using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Booking.Search;

namespace TABP.Application.Filters.ExpressionBuilders;

public static class BookingExpressionBuilder
{
    public static Expression<Func<RoomBooking, bool>> Build(BookingSearchQuery query, Guid? userId = null)
    {
        var filter = Expressions.True<RoomBooking>();

        filter = filter
        .AndIf(query.CheckInDate.HasValue,
            booking => booking.CheckInDate == query.CheckInDate.Value)
        .AndIf(query.CheckOutDate.HasValue,
            booking => booking.CheckOutDate == query.CheckOutDate.Value)
        .AndIf(query.CheckInDateFrom.HasValue,
            booking => booking.CheckInDate >= query.CheckInDateFrom.Value)
        .AndIf(query.CheckInDateTo.HasValue,
            booking => booking.CheckInDate <= query.CheckInDateTo.Value)
        .AndIf(query.MinPrice.HasValue,
            booking => booking.TotalPrice >= query.MinPrice.Value)
        .AndIf(query.MaxPrice.HasValue,
            booking => booking.TotalPrice <= query.MaxPrice.Value)
        .AndIf(!string.IsNullOrWhiteSpace(query.Notes),
            booking => booking.Notes.Contains(query.Notes!))
        .AndIf(query.Status != default,
            booking => booking.Status == query.Status)
        .AndIf(query.RoomNumber > 0,
            booking => booking.Room.Number == query.RoomNumber)
        .AndIf(query.HotelId != Guid.Empty,
            booking => booking.Room.HotelId == query.HotelId)
        .AndIf(userId.HasValue && userId != Guid.Empty,
            booking => booking.UserId == userId.Value);

        return filter;
    }
}