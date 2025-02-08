using FluentValidation;
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
    private readonly ILogger<HotelVisitService> _logger;
    private IValidator<VisitTimeOptionQuery> _timeOptionsValidator;

    
    public HotelVisitService(
        IHotelVisitRepository hotelVisitRepository,
        ILogger<HotelVisitService> logger,
        IValidator<VisitTimeOptionQuery> timeOptionsValidator)
    {
        _hotelVisitRepository = hotelVisitRepository;
        _logger = logger;
        _timeOptionsValidator = timeOptionsValidator;
    }

    public async Task AddAsync(HotelVisitDTO newHotelVisit) =>
         await _hotelVisitRepository.AddAsync(newHotelVisit);

    public async Task<IEnumerable<VisitedHotelDTO>> GetTop5VisitedHotels(
        VisitTimeOptionQuery query)
    {
        _timeOptionsValidator.ValidateAndThrow(query);

        var expression = TimeOptionExpressionBuilder<HotelVisit>.Build(query);

        _logger.LogInformation("Requested Top 5 Visited Hotels With Time Query: {query}", 
            query);

        return await _hotelVisitRepository
            .GetTop5VisitedHotels(expression);
    }
}