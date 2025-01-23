// using System.Linq.Expressions;
// using TABP.Application.Extensions;
// using TABP.Domain.Entities;
// using TABP.Domain.Models.Hotel;

// namespace TABP.Application.Filters.ExpressionBuilders;

// public static class HotelExpressionBuilder
// {
//     public static Expression<Func<Hotel, bool>> Build(HotelSearchQuery query)
//         {
//             var filter = Expressions.True<Hotel>();

//             filter = filter
//                 .AndIf(HasValidSearchTerm(query), 
//                     GetSearchTermFilter(query.SearchTerm))
//                 .AndIf(query.MinPrice > 0,
//                     hotel => hotel.Rooms.Any(room => room.PricePerNight >= query.MinPrice))
//                 .AndIf(query.MaxPrice > 0,
//                     hotel => hotel.Rooms.Any(room => room.PricePerNight <= query.MaxPrice))
//                 .AndIf(query.MinStars.HasValue,
//                     hotel => hotel.StarRating >= query.MinStars.Value)
//                 .AndIf(query.MaxStars.HasValue,
//                     hotel => hotel.StarRating <= query.MaxStars.Value)
//                 .AndIf(query.RoomTypes?.Any() == true,
//                     hotel => hotel.Rooms.Any(room => query.RoomTypes.Contains(room.Type)))
//                 .AndIf(query.CheckInDate != default && query.CheckOutDate != default,
//                     hotel => hotel.Rooms.Any(room => 
//                         !room.RoomBookings.Any(booking => 
//                             booking.CheckInDate < query.CheckOutDate && booking.CheckOutDate > query.CheckInDate)))
//                 .AndIf(query.NumberOfRooms > 0,
//                     hotel => hotel.Rooms.Count() >= query.NumberOfRooms)
//                 .AndIf(query.NumberOfAdults > 0 || query.NumberOfChildren > 0,
//                     hotel => hotel.Rooms.Any(room => 
//                         room.AdultsCapacity >= query.NumberOfAdults && 
//                         room.ChildrenCapacity >= query.NumberOfChildren))
//                 .AndIf(!string.IsNullOrWhiteSpace(query.City),
//                     hotel => hotel.City.Name.Contains(query.City!));

//             return filter;
//         }

//         private static bool HasValidSearchTerm(HotelSearchQuery query) =>
//             !string.IsNullOrWhiteSpace(query.SearchTerm);
//         private static Expression<Func<Hotel, bool>> GetSearchTermFilter(string searchTerm) =>
//             hotel => hotel.Name.Contains(searchTerm) || hotel.BriefDescription.Contains(searchTerm);
            
        
// }