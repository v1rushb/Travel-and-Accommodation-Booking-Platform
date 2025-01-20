using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Room.Search;

namespace TABP.Application.Filters.ExpressionBuilders;

public static class RoomForAdminExpressionBuilder
{
    public static Expression<Func<Room, bool>> Build(RoomSearchQuery query)
    {
        var filter = Expressions.True<Room>();

        filter = filter
            .AndIf(query.Number > 0,
                room => room.Number == query.Number)
            .AndIf(query.Type != default,
                room => room.Type == query.Type)
            .AndIf(query.AdultsCapacity > 0,
                room => room.AdultsCapacity >= query.AdultsCapacity)
            .AndIf(query.ChildrenCapacity >= 0,
                room => room.ChildrenCapacity >= query.ChildrenCapacity)
            .AndIf(query.PricePerNight > 0,
                room => room.PricePerNight <= query.PricePerNight);

        return filter;
    }
}