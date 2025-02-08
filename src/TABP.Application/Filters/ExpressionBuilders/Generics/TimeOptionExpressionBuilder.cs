using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Abstractions.Services.Generics;
using TABP.Domain.Enums;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Application.Filters.ExpressionBuilders.Generics;

public static class TimeOptionExpressionBuilder<T> where T : class, IHasCreationDate
{
    public static Expression<Func<T, bool>> Build(VisitTimeOptionQuery query)
    {
        var filter = Expressions.True<T>();

        filter = filter
            .And(GetTimeOptionFilter(query));

        return filter;
    }

    private static Expression<Func<T, bool>> GetTimeOptionFilter(VisitTimeOptionQuery query)
    {
        if(string.IsNullOrEmpty(query.TimeOption))
            return visit => true;

        var timeOption = Enum.Parse<TimeOptions>(query.TimeOption!, true);

        DateTime? filterDate = timeOption switch
        {
            TimeOptions.Today => DateTime.Now.Date,
            TimeOptions.LastWeek => DateTime.Now.AddDays(-7).Date,
            TimeOptions.LastMonth => DateTime.Now.AddMonths(-1).Date,
            TimeOptions.LastYear => DateTime.Now.AddYears(-1).Date,
            _ => null,
        };
        return filterDate.HasValue ? visit =>
            visit.CreationDate.Date >= filterDate.Value
            : visit => true;
    }

}