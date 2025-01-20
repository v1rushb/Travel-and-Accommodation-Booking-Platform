using AutoMapper;
using TABP.Domain.Models.HotelReview;

namespace TABP.API.Profiles.Review;

internal class ReviewProfiles : Profile
{
    public ReviewProfiles()
    {
        CreateMap<HotelReviewForCreationDTO, HotelReviewDTO>(); 
    }
}