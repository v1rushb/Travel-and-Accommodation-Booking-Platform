using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Booking.Search;

namespace TABP.Application.Filters.ExpressionBuilders;

public static class BookingExpressionBuilder
{
    public static Expression<Func<RoomBooking, bool>> Build(BookingSearchQuery query, Guid? userId = null)
    {
        var filter = Expressions.True<RoomBooking>();

        filter = filter.AndIf(
            HasValidDateRange(query),
            GetDateRangeFilter(query.CheckInDate, query.CheckOutDate)
        );

        filter = filter
            .AndIf(
                HasValidPriceRange(query),
                GetPriceRangeFilter(query.MinPrice, query.MaxPrice)
            )
            .AndIf(
                !HasValidPriceRange(query) && HasValidMinPrice(query),
                GetMinPriceFilter(query.MinPrice)
            )
            .AndIf(
                !HasValidPriceRange(query) && HasValidMaxPrice(query),
                GetMaxPriceFilter(query.MaxPrice)
            );

        filter = filter.AndIf(
            HasValidNotes(query.Notes),
            GetNotesFilter(query.Notes!)
        );

        filter = filter.AndIf(
            HasValidRoomNumber(query.RoomNumber),
            GetRoomNumberFilter(query.RoomNumber)
        )
        .AndIf(!HasValidRoomNumber(query.RoomNumber),
            GetRoomNumberFilter(-1)
        );

        filter = filter.AndIf(
            HasValidHotelId(query?.HotelId),
            GetHotelIdFilter(query?.HotelId)
        );

        filter = filter
            .AndIf(HasValidUserId(userId),
                GetUserIdFilter(userId));

        filter = filter
            .And(GetIdFilter(query?.Id));

        return filter;
    }

    private static bool HasValidDateRange(BookingSearchQuery query) =>
        query.CheckInDate.HasValue || query.CheckOutDate.HasValue;

    private static Expression<Func<RoomBooking, bool>> GetDateRangeFilter(DateTime? inDate, DateTime? outDate)
    {
        return booking =>
            (!inDate.HasValue || (inDate.Value >= booking.CheckInDate && inDate.Value <= booking.CheckOutDate) || booking.CheckOutDate >= inDate.Value) &&
            (!outDate.HasValue || (outDate.Value >= booking.CheckInDate && outDate.Value <= booking.CheckOutDate) || booking.CheckInDate <= outDate.Value);
    }

    private static bool HasValidPriceRange(BookingSearchQuery query) =>
        query.MinPrice > 0 && query.MaxPrice < decimal.MaxValue;

    private static bool HasValidMinPrice(BookingSearchQuery query) =>
        query.MinPrice > 0;

    private static bool HasValidMaxPrice(BookingSearchQuery query) =>
        query.MaxPrice < decimal.MaxValue;

    private static Expression<Func<RoomBooking, bool>> GetPriceRangeFilter(decimal min, decimal max) =>
        booking => booking.TotalPrice >= min && booking.TotalPrice <= max;

    private static Expression<Func<RoomBooking, bool>> GetMinPriceFilter(decimal min) =>
        booking => booking.TotalPrice >= min;

    private static Expression<Func<RoomBooking, bool>> GetMaxPriceFilter(decimal max) =>
        booking => booking.TotalPrice <= max;

    private static bool HasValidNotes(string notes) =>
        !string.IsNullOrWhiteSpace(notes);
    private static Expression<Func<RoomBooking, bool>> GetNotesFilter(string notes) =>
        booking => booking.Notes.Contains(notes);

    private static bool HasValidRoomNumber(int roomNumber) =>
        roomNumber >= 0;
    private static Expression<Func<RoomBooking, bool>> GetRoomNumberFilter(int roomNumber)
    {
        if(roomNumber == -1)
            return booking => true;

        return booking => booking.Room.Number == roomNumber;
    }

    private static bool HasValidHotelId(Guid? hotelId) =>
        hotelId.HasValue && hotelId != Guid.Empty;
    private static Expression<Func<RoomBooking, bool>> GetHotelIdFilter(Guid? hotelId)
    {
        if(hotelId.HasValue)
            return booking => booking.Room.HotelId == hotelId;
            
        return booking => true;
    }

    private static bool HasValidUserId(Guid? userId) =>
        userId.HasValue && userId != Guid.Empty;
    private static Expression<Func<RoomBooking, bool>> GetUserIdFilter(Guid? userId) =>
        booking => booking.UserId == userId;

    private static Expression<Func<RoomBooking, bool>> GetIdFilter(Guid? Id)
    {
        if(Id.HasValue)
            return booking => booking.Id == Id;
        return booking => true;
    }
}
