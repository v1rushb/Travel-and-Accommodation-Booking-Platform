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
        CreateMap<HotelReviewDTO, HotelReview>()
            .ForMember(dest => dest.Rating,
                opt => opt.MapFrom(src => (HotelRating)(int)src.Rating));
        
        CreateMap<HotelReview, HotelReviewDTO>()
            .ForMember(dest => dest.Rating,
                opt => opt.MapFrom(src => (decimal)src.Rating));

        CreateMap<HotelReview, HotelReviewUserResponseDTO>()
            .ForMember(dest => dest.Rating,
                opt => opt.MapFrom(src => src.Rating));

        CreateMap<HotelReview, HotelReviewAdminResponseDTO>()
            .ForMember(dest => dest.Rating,
                opt => opt.MapFrom(src => src.Rating));

        CreateMap<HotelReviewDTO, HotelReviewAdminResponseDTO>()
            .ForMember(dest => dest.Rating,
                opt => opt.MapFrom(src => (HotelRating)(int)src.Rating));

        CreateMap<HotelReviewDTO, HotelReviewUserResponseDTO>()
            .ForMember(dest => dest.Rating,
                opt => opt.MapFrom(src => (HotelRating)(int)src.Rating));
    }

}