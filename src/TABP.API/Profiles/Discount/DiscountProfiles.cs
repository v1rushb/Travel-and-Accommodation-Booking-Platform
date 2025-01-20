using AutoMapper;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Discount.Search.Response;

namespace TABP.API.Profiles.Discount;

internal class DiscountProfiles : Profile
{
    public DiscountProfiles()
    {
        CreateMap<DiscountForCreationDTO, DiscountDTO>();
        CreateMap<DiscountDTO, DiscountForAdminWithoutIdResponseDTO>();
        CreateMap<DiscountForUpdateDTO, DiscountDTO>();
        CreateMap<DiscountDTO, DiscountForUpdateDTO>();
    }
}