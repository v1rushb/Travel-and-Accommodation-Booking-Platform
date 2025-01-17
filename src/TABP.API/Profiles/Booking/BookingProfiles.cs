using AutoMapper;
using TABP.Domain.Models.RoomBooking;

namespace TABP.API.Profiles.Booking;

internal class BookingProfiles : Profile
{
    public BookingProfiles()
    {
        CreateMap<RoomBookingDTO, RoomBookingForCreationDTO>();
        CreateMap<RoomBookingForCreationDTO, RoomBookingDTO>();
    }
}