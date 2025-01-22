using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Application.Services;

public class HotelVisitService : IHotelVisitService
{
    private readonly IHotelVisitRepository _hotelVisitRepository;
    private readonly ILogger<HotelVisitService> _logger;
    
    public HotelVisitService(
        IHotelVisitRepository hotelVisitRepository,
        ILogger<HotelVisitService> logger)
    {
        _hotelVisitRepository = hotelVisitRepository;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(HotelVisitDTO newHotelVisit)
    {
        // do validations here.
        var visitId = await _hotelVisitRepository.AddAsync(newHotelVisit);

        _logger.LogInformation("Added HotelVisit for HotelId: {HotelId}, UserId: {UserId}", newHotelVisit.HotelId, newHotelVisit.UserId);
        
        return visitId;
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);

        await _hotelVisitRepository.DeleteAsync(Id);

        _logger.LogInformation("Deleted HotelVisit with Id: {Id}", Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _hotelVisitRepository.ExistsAsync(Id);

    public async Task<IEnumerable<HotelVisit>> GetByHotelAsync(Guid HotelId, DateTime? startDate = null, DateTime? endDate = null) =>
        await _hotelVisitRepository.GetByHotelAsync(HotelId, startDate, endDate);

    public async Task<HotelVisit> GetByIdAsync(Guid Id) =>
        await _hotelVisitRepository.GetByIdAsync(Id);

    public async Task<IEnumerable<HotelVisit>> GetByUserAsync(Guid userId, DateTime? startDate = null, DateTime? endDate = null) =>
        await _hotelVisitRepository.GetByUserAsync(userId, startDate, endDate);
    
    public async Task<IEnumerable<HotelVisit>> GetByUserAndHotelAsync(Guid userId, Guid hotelId, DateTime? startDate = null, DateTime? endDate = null) =>
        await _hotelVisitRepository.GetByUserAndHotelAsync(userId, hotelId, startDate, endDate);
    public async Task UpdateAsync(HotelVisitDTO updatedHotelVisit)
    {
        await ValidateId(updatedHotelVisit.Id);
        updatedHotelVisit.ModificationDate = DateTime.UtcNow;

        await _hotelVisitRepository.UpdateAsync(updatedHotelVisit);

        _logger.LogInformation("Updated HotelVisit with Id: {Id}", updatedHotelVisit.Id);
    }

    private async Task ValidateId(Guid Id)
    {
        if(! await ExistsAsync(Id))
        {
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
        }
    }
}