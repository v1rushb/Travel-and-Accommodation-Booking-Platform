using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Constants.Expression;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Hotel.Search;

namespace TABP.Application.Filters.ExpressionBuilders;

public static class HotelExpressionBuilder
{
    public static Expression<Func<Hotel, bool>> Build(HotelSearchQuery query)
{
    var filter = Expressions.True<Hotel>();

    filter = filter.AndIf(
        HasValidSearchTerm(query),
        GetSearchTermFilter(query.SearchTerm)
    );

    filter = filter
        .AndIf(
            HasValidPriceRange(query),
            GetPriceRangeFilter(query.MinPricePerNight, query.MaxPricePerNight)
        )
        .AndIf(
            !HasValidPriceRange(query) && HasValidMinPrice(query),
            GetMinPriceFilter(query.MinPricePerNight)
        )
        .AndIf(
            !HasValidPriceRange(query) && HasValidMaxPrice(query),
            GetMaxPriceFilter(query.MaxPricePerNight)
        );

    filter = filter
        .AndIf(
            HasValidRoomRange(query),
            GetRoomRangeFilter(query.MinNumberOfRooms, query.MaxNumberOfRooms)
        )
        .AndIf(
            !HasValidRoomRange(query) && HasValidMinRooms(query),
            GetMinRoomsFilter(query.MinNumberOfRooms)
        )
        .AndIf(
            !HasValidRoomRange(query) && HasValidMaxRooms(query),
            GetMaxRoomsFilter(query.MaxNumberOfRooms)
        );

    filter = filter
        .AndIf(
            query.MinStars.HasValue,
            GetMinStarsFilter(query.MinStars.GetValueOrDefault())
        )
        .AndIf(
            query.MaxStars.HasValue,
            GetMaxStarsFilter(query.MaxStars.GetValueOrDefault())
        );


    filter = filter
    .And(
        GetRoomTypesFilter(query.RoomTypes)
        );


    filter = filter.AndIf(
        query.CheckInDate.HasValue || query.CheckOutDate.HasValue,
        GetDateRangeFilter(query.CheckInDate, query.CheckOutDate)
    );


    filter = filter.AndIf(
        HasValidCapacity(query),
        GetCapacityFilter(query.NumberOfAdults, query.NumberOfChildren)
    );

    filter = filter.AndIf(
        HasValidCity(query),
        GetCityFilter(query.City!)
    );

    filter = filter
        .And(GetIdFilter(query?.Id));

    return filter;
}

    private static Expression<Func<Hotel, bool>> GetIdFilter(Guid? Id)
    {
        if(Id.HasValue)
            return hotel => hotel.Id == Id;
        
        return hotel => true;
    }

    private static Expression<Func<Hotel, bool>> GetMinStarsFilter(int minStars) =>
        hotel => hotel.StarRating >= minStars;

    private static Expression<Func<Hotel, bool>> GetMaxStarsFilter(int maxStars) =>
        hotel => hotel.StarRating <= maxStars;

    private static bool HasValidSearchTerm(HotelSearchQuery query) =>
        !string.IsNullOrWhiteSpace(query.SearchTerm);

    private static Expression<Func<Hotel, bool>> GetSearchTermFilter(string term) =>
        hotel => hotel.Name.Contains(term) ||
            hotel.BriefDescription.Contains(term);

    private static bool HasValidPriceRange(HotelSearchQuery query) =>
        query.MinPricePerNight > 0 &&
             query.MaxPricePerNight < int.MaxValue;

    private static bool HasValidMinPrice(HotelSearchQuery query) =>
        query.MinPricePerNight > 0;

    private static bool HasValidMaxPrice(HotelSearchQuery query) =>
        query.MaxPricePerNight < int.MaxValue;

    private static Expression<Func<Hotel, bool>> GetPriceRangeFilter(int min, int max) =>
        hotel => hotel.Rooms
            .Any(room => room.PricePerNight >= min && room.PricePerNight <= max);

    private static Expression<Func<Hotel, bool>> GetMinPriceFilter(int min) =>
        hotel => hotel.Rooms
            .Any(room => room.PricePerNight >= min);

    private static Expression<Func<Hotel, bool>> GetMaxPriceFilter(int max) =>
        hotel => hotel.Rooms
            .Any(room => room.PricePerNight <= max);

    private static bool HasValidRoomRange(HotelSearchQuery query) =>
        query.MinNumberOfRooms > 0 && 
            query.MaxNumberOfRooms < int.MaxValue;

    private static bool HasValidMinRooms(HotelSearchQuery query) =>
        query.MinNumberOfRooms > 0;

    private static bool HasValidMaxRooms(HotelSearchQuery query) =>
        query.MaxNumberOfRooms < int.MaxValue;

    private static Expression<Func<Hotel, bool>> GetRoomRangeFilter(int min, int max) =>
        hotel => hotel.Rooms.Count() >= min &&
             hotel.Rooms.Count() <= max;

    private static Expression<Func<Hotel, bool>> GetMinRoomsFilter(int min) =>
        hotel => hotel.Rooms
            .Count() >= min;

    private static Expression<Func<Hotel, bool>> GetMaxRoomsFilter(int max) =>
        hotel => hotel.Rooms
            .Count() <= max;

    private static Expression<Func<Hotel, bool>> GetRoomTypesFilter(IEnumerable<int> roomTypes)
    {
        if(roomTypes == null || !roomTypes.Any())
            return hotel => true;
        
        var validRoomTypes = roomTypes
            .Where(type => Enum.IsDefined(typeof(RoomType), type))
            .ToList();
        
        if(!validRoomTypes.Any())
            return hotel => false;

        return hotel => hotel.Rooms.Any(room =>
            validRoomTypes.Contains((int)room.Type));
    }


    private static Expression<Func<Hotel, bool>> GetDateRangeFilter(DateTime? inDate, DateTime? outDate)
    {
        return hotel => hotel.Rooms.Any(room =>
            !room.RoomBookings.Any(booking =>
                (!inDate.HasValue || booking.CheckOutDate > inDate.Value) &&
                (!outDate.HasValue || booking.CheckInDate < outDate.Value)));
    }



    private static bool HasValidCapacity(HotelSearchQuery query) =>
        query.NumberOfAdults > HotelSearchQueryConstants.NumberOfAdults ||
            query.NumberOfChildren > HotelSearchQueryConstants.NumberOfChildren;

    private static Expression<Func<Hotel, bool>> GetCapacityFilter(int adults, int children) =>
        hotel => hotel.Rooms.Count ==0 ||
                hotel.Rooms.Any(room =>
                    room.AdultsCapacity >= adults &&
                    room.ChildrenCapacity >= children);

    private static bool HasValidCity(HotelSearchQuery query) =>
        !string.IsNullOrWhiteSpace(query.City);

    private static Expression<Func<Hotel, bool>> GetCityFilter(string city) =>
        hotel => hotel.City.Name.Contains(city);
}
