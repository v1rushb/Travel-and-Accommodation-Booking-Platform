using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders.Generics;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Application.Services;

public class HotelVisitService : IHotelVisitService
{
    private readonly IHotelVisitRepository _hotelVisitRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<HotelVisitService> _logger;

    
    public HotelVisitService(
        IHotelVisitRepository hotelVisitRepository,
        ILogger<HotelVisitService> logger,
        ICurrentUserService currentUserService)
    {
        _hotelVisitRepository = hotelVisitRepository;
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task AddAsync(HotelVisitDTO newHotelVisit)
    {
        var visitId = await _hotelVisitRepository.AddAsync(newHotelVisit);

        _logger.LogInformation("Added HotelVisit for HotelId: {HotelId}, UserId: {UserId}", newHotelVisit.HotelId, newHotelVisit.UserId);
        
        // return visitId;
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _hotelVisitRepository.ExistsAsync(Id);

    // public async Task<IEnumerable<HotelVisit>> GetByHotelAsync(Guid HotelId, DateTime? startDate = null, DateTime? endDate = null) =>
    //     await _hotelVisitRepository.GetByHotelAsync(HotelId, startDate, endDate);

    // public async Task<IEnumerable<HotelVisit>> GetByUserAsync(Guid userId, DateTime? startDate = null, DateTime? endDate = null) =>
    //     await _hotelVisitRepository.GetByUserAsync(userId, startDate, endDate);
    
    // public async Task<IEnumerable<HotelVisit>> GetByUserAndHotelAsync(Guid userId, Guid hotelId, DateTime? startDate = null, DateTime? endDate = null) =>
    //     await _hotelVisitRepository.GetByUserAndHotelAsync(userId, hotelId, startDate, endDate);

    public async Task<IEnumerable<VisitedHotelDTO>> GetTop5VisitedHotels(VisitTimeOptionQuery query)
    {
        var expression = TimeOptionExpressionBuilder<HotelVisit>.Build(query);

        return await _hotelVisitRepository
            .GetTop5VisitedHotels(expression);
    }
}