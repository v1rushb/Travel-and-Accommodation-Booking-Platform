using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotels;

namespace TABP.Infrastructure.Extensions.Helpers;

public static class HotelExtensions
{
    public static IQueryable<HotelInsightDTO> WithInsights(
        this IQueryable<Hotel> hotels,
        Expression<Func<RoomBooking, bool>> timeOption
        )
    {
        timeOption ??= hotel => false;

        Expression<Func<RoomBooking, bool>> lambdaPredicate =
            timeOption.Body is UnaryExpression unary
                ? Expression.Lambda<Func<RoomBooking, bool>>(unary.Operand, timeOption.Parameters)
                : timeOption;

        return hotels
            .Select(hotel => new HotelInsightDTO
            {
                Id = hotel.Id,
                Name = hotel.Name,
                BriefDescription = hotel.BriefDescription,
                DetailedDescription = hotel.DetailedDescription,
                StarRating = hotel.StarRating,
                OwnerName = hotel.OwnerName,
                Geolocation = hotel.Geolocation,
                CreationDate = hotel.CreationDate,
                ModificationDate = hotel.ModificationDate,
                CityId = hotel.CityId,
                Revenue = hotel.Rooms.Sum(room =>
                room.RoomBookings
                    .AsQueryable()
                    .Where(lambdaPredicate)
                    .Sum(booking => booking.TotalPrice)
                ),
                CityName = hotel.City.Name
            });
    }

}