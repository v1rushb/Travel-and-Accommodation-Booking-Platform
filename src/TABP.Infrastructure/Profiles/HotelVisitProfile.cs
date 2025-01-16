using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Infrastructure.Profiles;

internal class HotelVisitProfile : Profile
{
    public HotelVisitProfile()
    {
        CreateMap<HotelVisitDTO, HotelVisit>();
        CreateMap<HotelVisit, HotelVisitDTO>();
    }
}