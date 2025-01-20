using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotels;

namespace TABP.Infrastructure.Profiles;

internal class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<HotelDTO, Hotel>();
        CreateMap<Hotel, HotelDTO>();
    
        CreateMap<Hotel, HotelUserResponseDTO>();
        CreateMap<HotelUserResponseDTO, Hotel>();
    }
}