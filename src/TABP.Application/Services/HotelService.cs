using FluentValidation;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel.Search;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotels;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IValidator<HotelDTO> _hotelValidator;
    private readonly IValidator<PaginationDTO> _paginationValidator;

    public HotelService(
        IHotelRepository hotelRepository,
        IValidator<HotelDTO> hotelValidator,
        IValidator<PaginationDTO> paginationValidator)
    {
        _hotelRepository = hotelRepository;
        _hotelValidator = hotelValidator;
        _paginationValidator = paginationValidator;
    }
    public async Task<Guid> AddAsync(HotelDTO newHotel)
    {
        await _hotelValidator.ValidateAndThrowAsync(newHotel);

        newHotel.CreationDate = DateTime.UtcNow;
        newHotel.ModificationDate = DateTime.UtcNow;

        return await _hotelRepository.AddAsync(newHotel);
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);

        await _hotelRepository.DeleteAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _hotelRepository.ExistsAsync(Id);

    public async Task<HotelDTO> GetByIdAsync(Guid Id) {
        await ValidateId(Id);
        return await _hotelRepository.GetByIdAsync(Id);
    }

    public async Task UpdateAsync(HotelDTO updatedHotel)
    {
        await _hotelValidator.ValidateAndThrowAsync(updatedHotel);
        
        updatedHotel.ModificationDate = DateTime.UtcNow;
        await _hotelRepository.UpdateAsync(updatedHotel);
    }

    public async Task<IEnumerable<HotelUserResponseDTO>> SearchAsync(
        HotelSearchQuery query,
        PaginationDTO pagination) 
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = HotelExpressionBuilder.Build(query);
        return await _hotelRepository.SearchAsync(expression, pagination.PageNumber, pagination.PageSize);
    }

    private async Task ValidateId(Guid Id)
    {
        if(!await ExistsAsync(Id))
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
    }

    public async Task<IEnumerable<HotelAdminResponseDTO>> SearchAdminAsync(
        HotelSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = HotelExpressionBuilder.Build(query);
        return await _hotelRepository.SearchAdminAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
    }

    public async Task<int> GetNextRoomNumberAsync(Guid hotelId) =>
        await _hotelRepository.GetNextRoomNumberAsync(hotelId);
}