using AutoMapper;
using TABP.Domain.Models.City;

namespace TABP.API.Profiles.City;

internal class CityProfiles : Profile
{
    public CityProfiles()
    {
        CreateMap<CityDTO, CityForCreationDTO>();
        CreateMap<CityForCreationDTO, CityDTO>();
    }
}