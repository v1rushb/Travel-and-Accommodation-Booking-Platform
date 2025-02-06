using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Enums;
using TABP.Domain.Models.Room.Search;

namespace TABP.Application.Filters.ExpressionBuilders;

public static class RoomExpressionBuilder
{
    public static Expression<Func<AvailableRoom, bool>> Build(RoomSearchQuery query)
    {
        var filter = Expressions.True<AvailableRoom>();

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
                HasValidAdultsCapacityRange(query),
                GetAdultsCapacityRangeFilter(query.MinAdultsCapacity, query.MaxAdultsCapacity)
            )
            .AndIf(
                !HasValidAdultsCapacityRange(query) && HasValidMinAdultsCapacity(query),
                GetMinAdultsCapacityFilter(query.MinAdultsCapacity)
            )
            .AndIf(
                !HasValidAdultsCapacityRange(query) && HasValidMaxAdultsCapacity(query),
                GetMaxAdultsCapacityFilter(query.MaxAdultsCapacity)
            );

        filter = filter
            .AndIf(
                HasValidChildrenCapacityRange(query),
                GetChildrenCapacityRangeFilter(query.MinChildrenCapacity, query.MaxChildrenCapacity)
            )
            .AndIf(
                !HasValidChildrenCapacityRange(query) && HasValidMinChildrenCapacity(query),
                GetMinChildrenCapacityFilter(query.MinChildrenCapacity)
            )
            .AndIf(
                !HasValidChildrenCapacityRange(query) && HasValidMaxChildrenCapacity(query),
                GetMaxChildrenCapacityFilter(query.MaxChildrenCapacity)
            );

        filter = filter
            .AndIf(
                HasValidNumberRange(query),
                GetNumberRangeFilter(query.MinNumber, query.MaxNumber)
            )
            .AndIf(
                !HasValidNumberRange(query) && HasValidMinNumber(query),
                GetMinNumberFilter(query.MinNumber)
            )
            .AndIf(
                !HasValidNumberRange(query) && HasValidMaxNumber(query),
                GetMaxNumberFilter(query.MaxNumber)
            );

        filter = filter.And(GetRoomTypesFilter(query.roomType));

        return filter;
    }

    private static bool HasValidNumberRange(RoomSearchQuery query) =>
        query.MinNumber > 0 && query.MaxNumber < int.MaxValue;

    private static bool HasValidMinNumber(RoomSearchQuery query) =>
        query.MinNumber > 0;

    private static bool HasValidMaxNumber(RoomSearchQuery query) =>
        query.MaxNumber < int.MaxValue;

    private static Expression<Func<AvailableRoom, bool>> GetNumberRangeFilter(int min, int max) =>
        room => room.Number >= min && room.Number <= max;

    private static Expression<Func<AvailableRoom, bool>> GetMinNumberFilter(int min) =>
        room => room.Number >= min;

    private static Expression<Func<AvailableRoom, bool>> GetMaxNumberFilter(int max) =>
        room => room.Number <= max;

    private static bool HasValidAdultsCapacityRange(RoomSearchQuery query) =>
        query.MinAdultsCapacity > 0 && query.MaxAdultsCapacity < int.MaxValue;

    private static bool HasValidMinAdultsCapacity(RoomSearchQuery query) =>
        query.MinAdultsCapacity > 0;

    private static bool HasValidMaxAdultsCapacity(RoomSearchQuery query) =>
        query.MaxAdultsCapacity < int.MaxValue;

    private static Expression<Func<AvailableRoom, bool>> GetAdultsCapacityRangeFilter(int min, int max) =>
        room => room.AdultsCapacity >= min && room.AdultsCapacity <= max;

    private static Expression<Func<AvailableRoom, bool>> GetMinAdultsCapacityFilter(int min) =>
        room => room.AdultsCapacity >= min;

    private static Expression<Func<AvailableRoom, bool>> GetMaxAdultsCapacityFilter(int max) =>
        room => room.AdultsCapacity <= max;

    private static bool HasValidChildrenCapacityRange(RoomSearchQuery query) =>
        query.MinChildrenCapacity > 0 && query.MaxChildrenCapacity < int.MaxValue;

    private static bool HasValidMinChildrenCapacity(RoomSearchQuery query) =>
        query.MinChildrenCapacity > 0;

    private static bool HasValidMaxChildrenCapacity(RoomSearchQuery query) =>
        query.MaxChildrenCapacity < int.MaxValue;

    private static Expression<Func<AvailableRoom, bool>> GetChildrenCapacityRangeFilter(int min, int max) =>
        room => room.ChildrenCapacity >= min && room.ChildrenCapacity <= max;

    private static Expression<Func<AvailableRoom, bool>> GetMinChildrenCapacityFilter(int min) =>
        room => room.ChildrenCapacity >= min;

    private static Expression<Func<AvailableRoom, bool>> GetMaxChildrenCapacityFilter(int max) =>
        room => room.ChildrenCapacity <= max;

    private static bool HasValidPriceRange(RoomSearchQuery query) =>
        query.MinPricePerNight >= 0 && query.MaxPricePerNight <= int.MaxValue;

    private static bool HasValidMinPrice(RoomSearchQuery query) =>
        query.MinPricePerNight > 0;

    private static bool HasValidMaxPrice(RoomSearchQuery query) =>
        query.MaxPricePerNight < int.MaxValue;

    private static Expression<Func<AvailableRoom, bool>> GetPriceRangeFilter(int min, int max) =>
        room => room.PricePerNight >= min && room.PricePerNight <= max;

    private static Expression<Func<AvailableRoom, bool>> GetMinPriceFilter(int min) =>
        room => room.PricePerNight >= min;

    private static Expression<Func<AvailableRoom, bool>> GetMaxPriceFilter(int max) =>
        room => room.PricePerNight <= max;

    private static Expression<Func<AvailableRoom, bool>> GetRoomTypesFilter(IEnumerable<int> roomTypes)
    {
        if (roomTypes == null || !roomTypes.Any())
            return room => true;

        var validTypes = roomTypes
            .Where(t => Enum.IsDefined(typeof(RoomType), t))
            .ToList();

        if (!validTypes.Any())
            return room => false;

        return room => validTypes.Contains((int)room.Type);
    }
}
