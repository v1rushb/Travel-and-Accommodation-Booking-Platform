using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILogger<RoomService> _logger;
    private readonly IValidator<RoomDTO> _roomValidator;
    private readonly IHotelRepository _hotelRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly IDiscountRepository _discountRepository;

    public RoomService(
        IRoomRepository roomRepository,
        ILogger<RoomService> logger,
        IValidator<RoomDTO> roomValidator,
        IHotelRepository hotelRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        IDiscountRepository discountRepository)
    {
        _roomRepository = roomRepository;
        _logger = logger;
        _roomValidator = roomValidator;
        _hotelRepository = hotelRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _discountRepository = discountRepository;
    }

    public async Task<Guid> AddAsync(RoomDTO newRoom) 
    {
        await _roomValidator.ValidateAndThrowAsync(newRoom);

        // var nextRoomNumber = await _hotelService.GetNextRoomNumberAsync(newRoom.HotelId);
        var nextRoomNumber = await _hotelRepository.GetNextRoomNumberAsync(newRoom.HotelId);
        
        newRoom.Number = nextRoomNumber;
        var roomId = await _roomRepository.AddAsync(newRoom);

        _logger.LogInformation("Added Room: {Number}, HotelId: {HotelId}, Id: {Id}", newRoom.Number, newRoom.HotelId, roomId);
        return roomId;
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);
        await _roomRepository.DeleteAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _roomRepository.ExistsAsync(Id);

    public async Task<RoomDTO?> GetByIdAsync(Guid Id)
    {
        await ValidateId(Id);

        return await _roomRepository.GetByIdAsync(Id);
    }

    public async Task<IEnumerable<Room>> GetRoomsByHotelAsync(Guid hotelId) =>
        await _roomRepository.GetRoomsByHotelAsync(hotelId);
        
    public async Task<bool> RoomNumberExistsForHotelAsync(Guid hotelId, Guid RoomId) =>
        await _roomRepository.RoomExistsForHotelAsync(hotelId, RoomId);

    public async Task UpdateAsync(RoomDTO updatedRoom)
    {
        //some validations here.

        updatedRoom.ModificationDate = DateTime.UtcNow;
        await _roomRepository.UpdateAsync(updatedRoom);
    }

    public async Task<IEnumerable<RoomAdminResponseDTO>> SearchAdminAsync(
        RoomSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = RoomExpressionBuilder.Build(query);
        return await _roomRepository.SearchAdminAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
    }

    public async Task<IEnumerable<RoomUserResponseDTO>> SearchRoomsAsync(
        RoomSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = RoomExpressionBuilder.Build(query);
        var rooms = await _roomRepository.SearchAdminAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);

        return _mapper.Map<IEnumerable<RoomUserResponseDTO>>(rooms);
    }

    private async Task ValidateId(Guid Id)
    {
        if(!await ExistsAsync(Id))
        {
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
        }
    }

    public async Task<decimal> GetBookingPriceForRoom(Guid roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        var room = await GetByIdAsync(roomId);

        var discount = await _discountRepository
            .GetHighestDiscountActiveForHotelRoomTypeAsync(room.HotelId, room.Type);
        
        return GetCalculatedDiscount(
            discount,
            room,
            checkInDate,
            checkOutDate);
    }

    private decimal GetCalculatedDiscount(
        DiscountDTO discount,
        RoomDTO room,
        DateTime checkInDate,
        DateTime checkOutDate)
    {
        var discountPercentage = discount?.AmountPercentage ?? 0;
        var originalPrice = ((checkInDate - checkOutDate).Days + 1) * room.PricePerNight;
        return ApplyDiscount(originalPrice, discountPercentage);
    }

    private static decimal ApplyDiscount(int originalPrice, decimal discountPercentage) =>
        originalPrice - (originalPrice * (discountPercentage / 100));
}