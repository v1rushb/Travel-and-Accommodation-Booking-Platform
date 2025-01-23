using AutoMapper;
using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.HotelReview.Search;

namespace TABP.Application.Profiles;

public class SearchProfile : Profile
{
    public SearchProfile()
    {
        // CreateMap<ReviewSearchQuery, AdminReviewSearchQuery>();
        // CreateMap<AdminReviewSearchQuery, ReviewSearchQuery>();
        CreateMap<AdminBookingSearchQuery, BookingSearchQuery>();
    }
}