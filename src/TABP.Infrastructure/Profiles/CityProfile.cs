using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.City;

namespace TABP.Infrastructure.Profiles;

internal class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<CityDTO, City>();
        CreateMap<City, CityDTO>();
    }
}