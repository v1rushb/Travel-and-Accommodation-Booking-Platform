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

    
    public HotelVisitService(
        IHotelVisitRepository hotelVisitRepository)
    {
        _hotelVisitRepository = hotelVisitRepository;
    }

    public async Task AddAsync(HotelVisitDTO newHotelVisit) =>
         await _hotelVisitRepository.AddAsync(newHotelVisit);

    public async Task<IEnumerable<VisitedHotelDTO>> GetTop5VisitedHotels(VisitTimeOptionQuery query)
    {
        var expression = TimeOptionExpressionBuilder<HotelVisit>.Build(query);

        return await _hotelVisitRepository
            .GetTop5VisitedHotels(expression);
    }
}