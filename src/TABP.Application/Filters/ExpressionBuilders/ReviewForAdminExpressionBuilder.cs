using System.Linq;
using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.HotelReview.Search;

namespace TABP.Application.Filters.ExpressionBuilders;

public static class ReviewForAdminExpressionBuilder
{
    public static Expression<Func<HotelReview, bool>> Build(AdminReviewSearchQuery query)
    {
        var filter = Expressions.True<HotelReview>();


        filter = 
            filter.AndIf(HasValidSearchTerm(query),
                GetSearchTermFilter(query.SearchTerm))
            
            .And(GetRatingFilter(query.Rating))

            .AndIf(HasValidCreationDate(query),
                GetCreationDateFilter(query.CreationDate))
            
            .AndIf(query.StartDate.HasValue || query.EndDate.HasValue, 
                GetFlexibleDateRangeFilter(query.StartDate, query.EndDate))

            .AndIf(HasValidHotelId(query),
                GetHotelIdFilter(query.HotelId))

            .AndIf(HasValidUserId(query.UserId),
                GetUserIdFilter(query));

            

        return filter;
    }

    private static bool HasValidSearchTerm(AdminReviewSearchQuery query) =>
        !string.IsNullOrWhiteSpace(query.SearchTerm);

    private static Expression<Func<HotelReview, bool>> GetSearchTermFilter(string searchTerm) =>
        review => review.Feedback.Contains(searchTerm);
    private static Expression<Func<HotelReview, bool>> GetRatingFilter(IEnumerable<int> ratings)
    {
        if (ratings == null || !ratings.Any())
        {
            return review => true; 
        }

        var validRatings = ratings
            .Where(rating => Enum.IsDefined(typeof(HotelRating), rating))
            .ToList();

        if (!validRatings.Any())
        {
            return review => false;
        }

        return review => validRatings.Contains((int)review.Rating);
    }

    private static bool HasValidCreationDate(AdminReviewSearchQuery query) =>
        query.CreationDate != default;

    private static Expression<Func<HotelReview, bool>> GetCreationDateFilter(DateTime creationDate) =>
        review => review.CreationDate >= creationDate;

    private static bool HasValidHotelId(AdminReviewSearchQuery query) =>
        query.HotelId != Guid.Empty;

    private static Expression<Func<HotelReview, bool>> GetHotelIdFilter(Guid hotelId) =>
        review => review.HotelId == hotelId;


    private static bool HasValidUserId(Guid? userId) =>
        userId.HasValue && userId != Guid.Empty;

    private static Expression<Func<HotelReview, bool>> GetUserIdFilter(AdminReviewSearchQuery query) =>
        review => review.UserId == query.UserId;
    
    private static Expression<Func<HotelReview, bool>> GetFlexibleDateRangeFilter(DateTime? startDate, DateTime? endDate)
    {
        return review =>
            (!startDate.HasValue || review.CreationDate >= startDate.Value) &&
            (!endDate.HasValue || review.CreationDate <= endDate.Value);
    }
}