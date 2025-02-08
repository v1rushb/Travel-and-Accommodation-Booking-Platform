using System.Linq.Expressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            .AndIf(HasValidId(query),
                GetIdFilter(query)
            );

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

    private static bool HasValidId(CitySearchQuery query) =>
        query.Id.HasValue;

    private static Expression<Func<City, bool>> GetIdFilter(CitySearchQuery query)
    {
        if(query.Id.HasValue)
            return city => city.Id == query.Id.Value;
        return city => true;
    }
}