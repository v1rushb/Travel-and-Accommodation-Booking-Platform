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
            .AndIf(HasValidSearchTerm(query),
                GetMinSeachTermFilter(query.SearchTerm)
            )

            .AndIf(HasValidCountry(query),
                GetCountryFilter(query.Country))
            ;

        return filter;
    }

    private static bool HasValidSearchTerm(CitySearchQuery query) =>
        !string.IsNullOrWhiteSpace(query.SearchTerm);
    private static Expression<Func<City, bool>> GetMinSeachTermFilter(string searchTerm) =>
        city => city.Name.Contains(searchTerm);

    private static bool HasValidCountry(CitySearchQuery query) =>
        !string.IsNullOrWhiteSpace(query.Country);

    private static Expression<Func<City, bool>> GetCountryFilter(string country) =>
        city => city.CountryName.Contains(country);
}