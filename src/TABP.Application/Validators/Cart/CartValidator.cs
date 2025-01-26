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

        RuleFor(cartItem => cartItem.CheckInDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Check-in date must be in the future.");

        RuleFor(cartItem => cartItem.CheckOutDate)
            .GreaterThan(cartItem => cartItem.CheckInDate)
            .WithMessage("Check-out date must be after Check-in date.");

        RuleFor(cartItem => cartItem)
            .MustAsync(async (cartItem, cancellationToken) =>
            {
                if (!await RoomNotAlreadyBookedByAnyUser(cartItem, cancellationToken))
                    return false;

                return await RoomNotAlreadyBooked(cartItem, cancellationToken);
            })
            .WithMessage(cartItem =>
            {
                return !RoomNotAlreadyBookedByAnyUser(cartItem, CancellationToken.None).Result
                    ? "This room is already booked during the selected dates."
                    : "You already have a booking with this room within the selected dates.";
            });
        
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