using AutoMapper;
using TABP.Domain.Models.Hotels;

namespace TABP.API.Profiles.Hotel;

internal class HotelProfiles : Profile
{
    public HotelProfiles()
    {
        CreateMap<HotelDTO, HotelForCreationDTO>();
        CreateMap<HotelForCreationDTO, HotelDTO>();
    }
}