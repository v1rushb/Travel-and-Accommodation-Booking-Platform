using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;

namespace TABP.Infrastructure.Profiles;

internal class HotelReviewProfile : Profile
{
    public HotelReviewProfile()
    {
        CreateMap<HotelReviewDTO, HotelReview>();
        CreateMap<HotelReview, HotelReviewDTO>();
    }
}