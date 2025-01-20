using AutoMapper;
using TABP.Domain.Models.City;
using TABP.Domain.Models.City.Response;
using TABP.Domain.Models.City.Search;

namespace TABP.API.Profiles.City;

internal class CityProfiles : Profile
{
    public CityProfiles()
    {
        CreateMap<CityDTO, CityForCreationDTO>();
        CreateMap<CityForCreationDTO, CityDTO>();

        CreateMap<CityDTO, CityForUpdateDTO>();
        CreateMap<CityForUpdateDTO, CityDTO>();

        CreateMap<CityDTO, CitySearchResponseDTO>();
    }
}