using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Discount.Search;

public static class DiscountForAdminExpressionBuilder
{
    public static Expression<Func<Discount, bool>> Build(DiscountSearchQuery query)
    {
        var filter = Expressions.True<Discount>();

        filter = filter.AndIf(
            HasValidSearchTerm(query),
            GetSearchTermFilter(query.SearchTerm!)
        );

        filter = filter.AndIf(
            HasValidDateRange(query),
            GetDateRangeFilter(query.StartingDate, query.EndingDate)
        );

        filter = filter
        .AndIf(
            HasValidPriceRange(query),
            GetPriceRangeFilter(query.MinAmountPercentage, query.MaxAmountPercentage)
        )
        .AndIf(
            !HasValidPriceRange(query) && HasValidMinPrice(query),
            GetMinPriceFilter(query.MinAmountPercentage)
        )
        .AndIf(
            !HasValidPriceRange(query) && HasValidMaxPrice(query),
            GetMaxPriceFilter(query.MaxAmountPercentage) 
        );

        filter = filter.And(
            GetRoomTypeFilter(query.RoomType)
        );

        filter = filter
            .And(GetIdFilter(query?.Id));

        return filter;
    }

    private static Expression<Func<Discount, bool>> GetIdFilter(Guid? Id)
    {
        if(Id.HasValue)
            return discount => discount.Id == Id;
        
        return discount => true;
    }

    private static bool HasValidSearchTerm(DiscountSearchQuery query) =>
        !string.IsNullOrWhiteSpace(query.SearchTerm);

    private static Expression<Func<Discount, bool>> GetSearchTermFilter(string term) =>
        discount => discount.Reason.Contains(term);

    private static bool HasValidDateRange(DiscountSearchQuery query) =>
        query.StartingDate != default || query.EndingDate != default;

    private static Expression<Func<Discount, bool>> GetDateRangeFilter(DateTime? inDate, DateTime? outDate)
    {
        return discount =>
            (!inDate.HasValue || (inDate.Value >= discount.CreationDate && inDate.Value <= discount.EndingDate) || discount.EndingDate >= inDate.Value) &&
            (!outDate.HasValue || (outDate.Value >= discount.CreationDate && outDate.Value <= discount.EndingDate) || discount.CreationDate <= outDate.Value);
    }

    private static Expression<Func<Discount, bool>> GetRoomTypeFilter(IEnumerable<int>? roomTypes)
    {
        if (roomTypes == null || !roomTypes.Any())
            return discount => true;

        var validTypes = roomTypes
            .Where(t => Enum.IsDefined(typeof(RoomType), t))
            .ToList();

        if (!validTypes.Any())
            return discount => false; 
        return discount => validTypes.Contains((int)discount.roomType);
    }

     private static bool HasValidPriceRange(DiscountSearchQuery query) =>
        query.MinAmountPercentage >= 0 && query.MaxAmountPercentage <= 100 ;

    private static bool HasValidMinPrice(DiscountSearchQuery query) =>
        query.MinAmountPercentage > 0;

    private static bool HasValidMaxPrice(DiscountSearchQuery query) =>
        query.MaxAmountPercentage <= 100; 

    private static Expression<Func<Discount, bool>> GetPriceRangeFilter(decimal min, decimal max) =>
        discount => discount.AmountPercentage >= min && discount.AmountPercentage <= max;

    private static Expression<Func<Discount, bool>> GetMinPriceFilter(decimal min) =>
        discount => discount.AmountPercentage >= min;

    private static Expression<Func<Discount, bool>> GetMaxPriceFilter(decimal max) =>
        discount => discount.AmountPercentage <= max;
}
