using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;

namespace TABP.Domain.Abstractions.Services.Review;

/// <summary>
/// Provides core operations for managing<see cref="HotelReview"/>.
/// </summary>
public interface IHotelReviewService
{
    /// <summary>
    /// Adds a new hotel review.
    /// </summary>
    /// <param name="newReview">
    /// The<see cref="HotelReviewDTO"/>containing the details of the<see cref="HotelReview"/>to add.
    /// </param>
    /// <exception cref="FluentValidation.ValidationException">
    /// Thrown if the review data is invalid.
    /// </exception>
    Task AddAsync(HotelReviewDTO newReview);

    /// <summary>
    /// Retrieves a hotel review by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="reviewId">
    /// The unique<see cref="Guid"/>identifier of the hotel review.
    /// </param>
    /// <returns>
    /// A<see cref="HotelReviewDTO"/>Representing the<see cref="HotelReview"/>.
    /// </returns>
    /// <exception cref="Exceptions.EntityNotFoundException">
    /// Thrown if the review does not exist.
    /// </exception>
    Task<HotelReviewDTO> GetByIdAsync(Guid reviewId);

    /// <summary>
    /// Updates an existing hotel review.
    /// </summary>
    /// <param name="updatedReview">
    /// The<see cref="HotelReviewDTO"/>containing updated<see cref="HotelReview"/>details.
    /// </param>
    /// <exception cref="FluentValidation.ValidationException">
    /// Thrown if the updated review data is invalid.
    /// </exception>
    /// <exception cref="Exceptions.EntityNotFoundException">
    /// Thrown if the review does not exist or does not belong to the current user.
    /// </exception>
    Task UpdateAsync(HotelReviewDTO updatedReview);

    /// <summary>
    /// Deletes a hotel review by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="reviewId">
    /// The unique<see cref="Guid"/>identifier of the<see cref="HotelReview"/>to delete.
    /// </param>
    /// <exception cref="Exceptions.EntityNotFoundException">
    /// Thrown if the review does not exist or does not belong to the current user.
    /// </exception>
    Task DeleteAsync(Guid reviewId);

    /// <summary>
    /// Determines whether a hotel review exists.
    /// </summary>
    /// <param name="reviewId">
    /// The unique<see cref="Guid"/>identifier of the hotel review.
    /// </param>
    /// <param name="userId">
    /// (Optional) The<see cref="Guid"/>identifier of the user to check ownership. If provided, the method verifies that the review belongs to this user.
    /// </param>
    /// <returns>
    /// <c>true</c> if the review exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid reviewId, Guid? userId = null);

    /// <summary>
    /// Retrieves all hotel reviews submitted by a specific user.
    /// </summary>
    /// <param name="userId">
    /// The unique<see cref="Guid"/>identifier of the user.
    /// </param>
    /// <returns>
    /// A collection of<see cref="HotelReview"/>.
    /// </returns>
    Task<IEnumerable<HotelReview>> GetReviewsByUserAsync(Guid userId);
}