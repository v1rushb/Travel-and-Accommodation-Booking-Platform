using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Discount.Search.Response;

namespace TABP.Infrastructure.Profiles;

internal class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<DiscountDTO, Discount>();
        CreateMap<Discount, DiscountDTO>();

        CreateMap<Discount, DiscountForAdminResponseDTO>();
    }
}