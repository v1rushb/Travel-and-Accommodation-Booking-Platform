using AutoMapper;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotels;

namespace TABP.API.Profiles.Hotel;

internal class HotelProfiles : Profile
{
    public HotelProfiles()
    {
        CreateMap<HotelDTO, HotelForCreationDTO>();
        CreateMap<HotelForCreationDTO, HotelDTO>();
        CreateMap<HotelDTO, HotelForUpdateDTO>();
        CreateMap<HotelForUpdateDTO, HotelDTO>();
        CreateMap<HotelDTO, HotelAdminWithoutIdResponseDTO>();
        CreateMap<HotelAdminWithoutIdResponseDTO, HotelDTO>();
    }
}