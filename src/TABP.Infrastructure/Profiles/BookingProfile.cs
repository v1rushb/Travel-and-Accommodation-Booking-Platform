using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Infrastructure.Profiles;

internal class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<RoomBookingDTO, RoomBooking>();
        CreateMap<RoomBooking, RoomBookingDTO>();

        CreateMap<RoomBooking, BookingUserResponseDTO>();
        CreateMap<RoomBooking, BookingAdminResponseDTO>();
        CreateMap<RoomBookingDTO ,BookingAdminResponseDTO>();
    }
}