using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Cart.Search;

public static class CartExpressionBuilder
{
    public static Expression<Func<Cart, bool>> Build(CartSearchQuery query, Guid? UserId = null)
    {
        var filter = Expressions.True<Cart>();

        filter = filter
            .AndIf(
                HasValidPriceRange(query),
                GetPriceRangeFilter(query.MinTotalPrice, query.MaxTotalPrice)
            )
            .AndIf(
                !HasValidPriceRange(query) && HasValidMinPrice(query),
                GetMinPriceFilter(query.MinTotalPrice)
            )
            .AndIf(
                !HasValidPriceRange(query) && HasValidMaxPrice(query),
                GetMaxPriceFilter(query.MaxTotalPrice)
            )
            .And(GetStatusFilter(query.Status))
            .AndIf(
                HasValidCheckOutDateRange(query),
                GetCheckOutDateRangeFilter(query.MinCheckOutDate, query.MaxCheckOutDate)
            )
            .AndIf(
                HasValidCreationDateRange(query),
                GetCreationDateRangeFilter(query.MinCreationDate, query.MaxCreationDate)
            )
            .AndIf(
                HasValidUserId(UserId), 
                GetUserIdFilter(UserId)
            );

        filter = filter
            .And(GetIdFilter(query?.Id));
        return filter;
    }

    private static Expression<Func<Cart, bool>> GetIdFilter(Guid? Id)
    {
        if(Id.HasValue)
            return cart => cart.Id == Id;
        
        return cart => true;
    }

    private static bool HasValidPriceRange(CartSearchQuery query) =>
        query.MinTotalPrice > 0 && 
            query.MaxTotalPrice < decimal.MaxValue;

    private static bool HasValidMinPrice(CartSearchQuery query) =>
        query.MinTotalPrice > 0;

    private static bool HasValidMaxPrice(CartSearchQuery query) =>
        query.MaxTotalPrice < decimal.MaxValue;

    private static Expression<Func<Cart, bool>> GetPriceRangeFilter(decimal min, decimal max) =>
        cart => cart.TotalPrice >= min &&
             cart.TotalPrice <= max;

    private static Expression<Func<Cart, bool>> GetMinPriceFilter(decimal min) =>
        cart => 
            cart.TotalPrice >= min;

    private static Expression<Func<Cart, bool>> GetMaxPriceFilter(decimal max) =>
        cart => 
            cart.TotalPrice <= max;

    private static Expression<Func<Cart, bool>> GetStatusFilter(IEnumerable<int>? statuses)
    {
        if (statuses == null || !statuses.Any())
            return cart => true;

        var validStatuses = statuses
            .Where(s => Enum.IsDefined(typeof(BookingStatus), s))
            .ToList();

        if (!validStatuses.Any())
            return cart => false;

        return cart => 
            validStatuses.Contains((int)cart.Status);
    }

    private static bool HasValidCheckOutDateRange(CartSearchQuery query) =>
        query.MinCheckOutDate.HasValue ||
             query.MaxCheckOutDate.HasValue;

    private static Expression<Func<Cart, bool>> GetCheckOutDateRangeFilter(DateTime? min, DateTime? max) =>
        cart =>
            (!min.HasValue || cart.CheckOutDate >= min.Value) &&
            (!max.HasValue || cart.CheckOutDate <= max.Value);

    private static bool HasValidCreationDateRange(CartSearchQuery query) =>
        query.MinCreationDate.HasValue || query.MaxCreationDate.HasValue;

    private static Expression<Func<Cart, bool>> GetCreationDateRangeFilter(DateTime? min, DateTime? max) =>
        cart =>
            (!min.HasValue || cart.CreationDate >= min.Value) &&
            (!max.HasValue || cart.CreationDate <= max.Value);

    private static bool HasValidUserId(Guid? userId) =>
        userId.HasValue &&
             userId != Guid.Empty;

    private static Expression<Func<Cart, bool>> GetUserIdFilter(Guid? userId) =>
        cart =>
             cart.UserId == userId;
}
