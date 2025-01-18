using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;

namespace TABP.Infrastructure.Repositories;

public class HotelReviewsRepository : IHotelReviewRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelReviewsRepository> _logger;

    public HotelReviewsRepository(
        HotelBookingDbContext context,
        IMapper mapper,
        ILogger<HotelReviewsRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    } 

    public async Task<Guid> AddAsync(HotelReviewDTO newReview)
    {
        var review = _mapper.Map<HotelReview>(newReview);
        review.CreationDate = DateTime.UtcNow;
        review.ModificationDate = DateTime.UtcNow;

        var entityEntry = _context.HotelReviews.Add(review);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created Review with Id: {ReviewId}, HotelId: {HotelId}, UserId: {UserId}", review.Id, review.HotelId, review.UserId); // who?
        
        return entityEntry.Entity.Id;
    }

    public async Task<HotelReview?> GetByIdAsync(Guid reviewId) =>
        await _context.HotelReviews.FirstOrDefaultAsync(review => review.Id == reviewId);

    public async Task UpdateAsync(HotelReviewDTO updatedReview)
    {
        var review = _mapper.Map<HotelReview>(updatedReview);
        review.ModificationDate = DateTime.UtcNow;

        _context.HotelReviews.Update(review);
        await _context.SaveChangesAsync();
         _logger.LogInformation("Updated Review with Id: {ReviewId}", updatedReview.Id);
    }

    public async Task DeleteAsync(Guid reviewId)
    {
        _context.HotelReviews.Remove(new HotelReview { Id = reviewId });
        await _context.SaveChangesAsync();
        _logger.LogInformation("Deleted Review with Id: {ReviewId}", reviewId);
    }

    public async Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid HotelId) =>
        await _context.HotelReviews.AnyAsync(review => review.UserId == userId && review.HotelId == HotelId);

    public async Task<decimal> GetReviewsByHotelCountAsync(Guid hotelId) =>
        await _context.HotelReviews.CountAsync(review => review.HotelId == hotelId);

    public async Task<double> GetAverageRatingByHotelAsync(Guid hotelId) =>
        await _context.HotelReviews
            .Where(review => review.HotelId == hotelId)
            .Select(review => (int) review.Rating)
            .AverageAsync(rating => rating);
    
    public async Task<IEnumerable<HotelReview>> GetReviewsByHotelAsync(Guid hotelId) =>
        await _context.HotelReviews.Where(review => review.HotelId == hotelId).ToListAsync();

    public async Task<IEnumerable<HotelReview>> GetReviewsByUserAsync(Guid userId) =>
        await _context.HotelReviews
            .Include(review => review.User)
            .Where(review => review.UserId == userId)
            .ToListAsync();
    
    public async Task<bool> ExistsAsync(Guid Id, Guid? userId = null) =>
        await _context.HotelReviews
            .AnyAsync(review => (review.Id == Id) && (!userId.HasValue || (review.UserId == userId.Value)));

    public async Task<HotelReview?> GetByUserAndHotelAsync(Guid userId, Guid hotelId) =>
        await _context.HotelReviews
            .FirstOrDefaultAsync(review => (review.UserId == userId) && (review.HotelId == hotelId));
        
}