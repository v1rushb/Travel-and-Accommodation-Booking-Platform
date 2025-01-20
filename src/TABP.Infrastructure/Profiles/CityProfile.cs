using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.City;
using TABP.Domain.Models.City.Response;
using TABP.Domain.Models.City.Search;

namespace TABP.Infrastructure.Profiles;

internal class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<CityDTO, City>();
        CreateMap<City, CityDTO>();

        CreateMap<City, CitySearchResponseDTO>();
        CreateMap<CitySearchResponseDTO, City>();

        CreateMap<City, CityAdminResponseDTO>();
    }
}