using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.HotelReview;
using TABP.Domain.Models.HotelReview.Search.Response;

namespace TABP.Infrastructure.Profiles;

internal class HotelReviewProfile : Profile
{
    public HotelReviewProfile()
    {
        CreateMap<HotelReviewDTO, HotelReview>();
        CreateMap<HotelReview, HotelReviewDTO>();
        CreateMap<HotelReview, HotelReviewUserResponseDTO>();
        CreateMap<HotelReview, HotelReviewAdminResponseDTO>();
        CreateMap<HotelReviewDTO, HotelReviewAdminResponseDTO>()
            .ForMember(dest => dest.Rating,
                opt => opt.MapFrom(src => (HotelRating)src.Rating));
    }
}