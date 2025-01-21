using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.CartItem;

namespace TABP.Application.Validators.Cart;

public class CartItemValidator : AbstractValidator<CartItemDTO>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICurrentUserService _currentUserService;
    public CartItemValidator(
        ICartRepository cartRepository,
        ICurrentUserService currentUserService)
    {
        _cartRepository = cartRepository;
        _currentUserService = currentUserService;   

        RuleFor(cartItem => cartItem)
            .MustAsync(RoomNotAlreadyBooked)
            .WithMessage("Room is already booked for this period.");

        RuleFor(cartItem => cartItem)
            .MustAsync(RoomNotAlreadyBookedByAnyUser)
            .WithMessage("This room is already booked during the selected dates.");
    }

    private async Task<bool> RoomNotAlreadyBooked(CartItemDTO cartItem, CancellationToken cancellationToken)
    {
        return !await _cartRepository.RoomIsBookedForSameUserBetween(
            cartItem.RoomId,
            _currentUserService.GetUserId(),
            cartItem.CheckInDate,
            cartItem.CheckOutDate);
    }

    private async Task<bool> RoomNotAlreadyBookedByAnyUser(CartItemDTO cartItem, CancellationToken cancellationToken)
    {
        return !await _cartRepository.RoomIsBookedByAnyUserBetween(
            cartItem.RoomId,
            cartItem.CheckInDate,
            cartItem.CheckOutDate);
    }
}