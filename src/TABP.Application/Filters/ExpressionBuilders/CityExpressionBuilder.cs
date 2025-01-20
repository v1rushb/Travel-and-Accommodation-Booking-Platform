using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Entities;
using TABP.Domain.Models.City.Search;

namespace TABP.Application.Filters.ExpressionBuilders;

public static class CityExpressionBuilder
{
    public static Expression<Func<City, bool>> Build(CitySearchQuery query)
    {
        var filter = Expressions.True<City>();

        filter = filter
            .AndIf(!string.IsNullOrWhiteSpace(query.SearchTerm),
                city => city.Name.Contains(query.SearchTerm))
            .AndIf(!string.IsNullOrWhiteSpace(query.Country),
                city => city.CountryName.Contains(query.Country));

        return filter;
    } // needs refactoring.
}