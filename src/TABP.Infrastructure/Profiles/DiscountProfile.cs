using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.Discount;

namespace TABP.Infrastructure.Profiles;

internal class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<DiscountDTO, Discount>();
        CreateMap<Discount, DiscountDTO>();
    }
}