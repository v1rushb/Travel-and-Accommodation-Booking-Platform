using AutoMapper;
using TABP.Domain.Entities;
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
    }
}