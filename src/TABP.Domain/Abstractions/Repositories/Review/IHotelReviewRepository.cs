using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;

namespace TABP.Domain.Abstractions.Repositories.Review;

/// <summary>
/// Defines the contract for a repository to manage <see cref="HotelReview"/> entities.
/// This interface provides asynchronous operations for creating, retrieving, updating, and deleting hotel review data,
/// as well as searching, counting, and checking for the existence of reviews based on various criteria.
/// </summary>
public interface IHotelReviewRepository
{
    /// <summary>
    /// Asynchronously adds a new hotel review to the repository.
    /// </summary>
    /// <param name="newReview">A <see cref="HotelReviewDTO"/> containing the data for the new hotel review.</param>
    /// <returns>A <see cref="Task{Guid}"/> representing the asynchronous operation, and upon completion,
    /// returns the unique identifier of the newly added hotel review.
    /// </returns>
    Task<Guid> AddAsync(HotelReviewDTO newReview);

    /// <summary>
    /// Asynchronously retrieves a hotel review from the repository by its unique identifier.
    /// </summary>
    /// <param name="reviewId">The unique identifier of the hotel review to retrieve.</param>
    /// <returns>A <see cref="Task{HotelReviewDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="HotelReviewDTO"/> if found; otherwise
    /// </returns>
    Task<HotelReviewDTO> GetByIdAsync(Guid reviewId);


    /// <summary>
    /// Asynchronously retrieves the count of reviews for a specific hotel.
    /// </summary>
    /// <param name="hotelId">The unique identifier of the hotel.</param>
    /// <returns>A <see cref="Task{decimal}"/> representing the asynchronous operation, and upon completion, 
    /// returns the count of reviews for the specified hotel.
    /// </returns>
    Task<decimal> GetReviewsByHotelCountAsync(Guid hotelId);


     /// <summary>
    /// Asynchronously checks if a review exists for a specific user and hotel combination.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="HotelId">The unique identifier of the hotel.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a review by the user for the hotel exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid HotelId);

    /// <summary>
    /// Asynchronously updates an existing hotel review in the repository.
    /// </summary>
    /// <param name="updatedReview">A <see cref="HotelReviewDTO"/> containing the updated data for the hotel review.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(HotelReviewDTO updatedReview);


    /// <summary>
    /// Asynchronously deletes a hotel review from the repository by its unique identifier.
    /// </summary>
    /// <param name="reviewId">The unique identifier of the hotel review to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid reviewId);


    /// <summary>
    /// Asynchronously calculates the average rating for a specific hotel.
    /// </summary>
    /// <param name="hotelId">The unique identifier of the hotel.</param>
    /// <returns>A <see cref="Task{double}"/> representing the asynchronous operation, and upon completion,
    /// returns the average rating for the hotel.
    /// </returns>
    Task<double> GetAverageRatingByHotelAsync(Guid hotelId);

    /// <summary>
    /// Asynchronously retrieves all reviews made by specific user.
    /// </summary>
    /// <param name="hotelId">The unique identifier of the user.</param>
    Task<IEnumerable<HotelReview>> GetReviewsByUserAsync(Guid userId);

     /// <summary>
    /// Asynchronously checks if a hotel review with the specified ID exists in the repository, optionally filtering by user ID.
    /// </summary>
    /// <param name="reviewId">The unique identifier of the hotel review to check.</param>
    /// <param name="userId">An optional user identifier to further refine the existence check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a hotel review with the given ID exists (optionally for the user); otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid reviewId, Guid? userId = null);

    /// <summary>
    /// Searches and retrieves a collection of hotel reviews based on a predicate, with pagination and optional sorting.
    /// </summary>
    /// <param name="predicate">An <see cref="Expression{Func{HotelReview, bool}}"/> that defines the search conditions for hotel reviews.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of hotel reviews per page.</param>
    /// <param name="orderByDelegate">An optional <see cref="Func{IQueryable{HotelReview}, IOrderedQueryable{HotelReview}}"/> delegate to specify the sorting order.</param>
    /// <returns>A <see cref="Task{IEnumerable{HotelReviewDTO}}"/> representing the asynchronous operation, and upon completion,
    /// returns a collection of <see cref="HotelReviewDTO"/> that match the search criteria.</returns>
    Task<IEnumerable<HotelReviewDTO>> SearchAsync(
        Expression<Func<HotelReview, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<HotelReview>, IOrderedQueryable<HotelReview>> orderByDelegate = null);
}