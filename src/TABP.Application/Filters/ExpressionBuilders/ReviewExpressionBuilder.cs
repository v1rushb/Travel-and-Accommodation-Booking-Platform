using System.Linq.Expressions;
using TABP.Application.Extensions;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.HotelReview.Search;

namespace TABP.Application.Filters.ExpressionBuilders;

public static class ReviewExpressionBuilder
{
    public static Expression<Func<HotelReview, bool>> Build(ReviewSearchQuery query, Guid? userId = null) // hmm maybe not?
    {
        var filter = Expressions.True<HotelReview>();

         filter = filter
            .AndIf(!string.IsNullOrWhiteSpace(query.SearchTerm),
                review => review.Feedback.Contains(query.SearchTerm!))
            .AndIf(query.Rating != default(HotelRating),
                review => review.Rating == query.Rating)
            .AndIf(query.CreationDate != default,
                review => review.CreationDate >= query.CreationDate)
            .AndIf(query.HotelId != Guid.Empty,
                review => review.HotelId == query.HotelId)
            .AndIf(userId.HasValue && userId != Guid.Empty,
                review => review.UserId == userId.Value);

        return filter;
    }
}