using AutoMapper;
using TABP.Domain.Enums;
using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.City.Response;
using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.HotelReview;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.Hotels;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.Room.Search.Response;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Application.Profiles;

public class SearchProfile : Profile
{
    public SearchProfile()
    {
        CreateMap<AdminBookingSearchQuery, BookingSearchQuery>();

        CreateMap<HotelInsightDTO, HotelUserResponseDTO>();
        CreateMap<RoomBookingDTO, RoomAdminResponseDTO>();
        CreateMap<RoomBookingDTO, BookingUserResponseDTO>();
        CreateMap<CitySearchResponseDTO, CityAdminResponseDTO>();
        CreateMap<HotelInsightDTO, HotelAdminResponseDTO>();
        CreateMap<HotelReviewDTO, HotelReviewUserResponseDTO>()
            .ForMember(dest => dest.Rating,
                opt => opt.MapFrom(src => (HotelRating)src.Rating));

        CreateMap<RoomDTO, RoomUserResponseDTO>();
        CreateMap<RoomDTO, RoomAdminResponseDTO>();
    }
}