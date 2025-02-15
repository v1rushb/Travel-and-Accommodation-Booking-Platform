using AutoMapper;
using TABP.Domain.Models.Discount;

namespace TABP.Application.Profiles;

public class DiscountProfiles : Profile
{
    public DiscountProfiles()
    {
        CreateMap<DiscountDTO, DiscountUserResponseDTO>();
    }
}