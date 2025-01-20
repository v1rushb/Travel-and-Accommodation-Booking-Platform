using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Discount.Search;

namespace TABP.Application.Filters.ExpressionBuilders;

public static class DiscountForAdminExpressionBuilder
{
    public static Expression<Func<Discount, bool>> Build(DiscountSearchQuery query)
    {
        var filter = Expressions.True<Discount>();

        filter = filter
            .AndIf(!string.IsNullOrWhiteSpace(query.SearchTerm),
                discount => discount.Reason.Contains(query.SearchTerm!))
            .AndIf(query.StartingDate != default,
                discount => discount.StartingDate >= query.StartingDate)
            .AndIf(query.EndingDate != default,
                discount => discount.EndingDate <= query.EndingDate)
            .AndIf(query.AmountPercentage > 0,
                discount => discount.AmountPercentage >= query.AmountPercentage)
            .AndIf(query.roomType != default,
                discount => discount.roomType == query.roomType);

        return filter;
    }
}