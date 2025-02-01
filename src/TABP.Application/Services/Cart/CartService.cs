using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Abstractions.Services.Cart;
using TABP.Domain.Enums;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.Cart.Search.Response;
using TABP.Domain.Models.CartItem;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.Cart;


public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IRoomBookingService _roomBookingService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly ILogger<CartService> _logger;
    private readonly IValidator<CartItemDTO> _cartItemValidator;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IRoomService _roomService;
    private readonly IUnitOfWork _unitOfWork;
    public CartService(
        ICartRepository cartRepository,
        IRoomBookingService roomBookingService,
        ICartItemRepository cartItemRepository,
        ICurrentUserService currentUserService,
        ILogger<CartService> logger,
        IValidator<CartItemDTO> cartItemValidator,
        IValidator<PaginationDTO> paginationValidator,
        IRoomService roomService,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _roomBookingService = roomBookingService;
        _cartItemRepository = cartItemRepository;
        _currentUserService = currentUserService;
        _logger = logger;
        _cartItemValidator = cartItemValidator;
        _paginationValidator = paginationValidator;
        _roomService = roomService;
        _unitOfWork = unitOfWork;
    }

    public async Task<CartDTO> CreateNewAsync() // make private?
    {
        var currentUserId = _currentUserService.GetUserId();
        var lastCart = await _cartRepository.GetLastPendingCartAsync(currentUserId);
        if(lastCart is not null)
        {
            lastCart.Status = BookingStatus.Cancelled;
            lastCart.ModificationDate = DateTime.UtcNow;

            _logger.LogInformation("Cart with Id {CartId} has been cancelled", lastCart.Id);

            await _cartRepository.UpdateAsync(lastCart);
        }


        var newCart = new CartDTO
        {
            UserId = currentUserId,
            Status = BookingStatus.Pending,
            CreationDate = DateTime.UtcNow,
            ModificationDate = DateTime.UtcNow,
        };

        // validate here.

        var newCartId = await _cartRepository.CreateAsync(newCart);
        newCart.Id = newCartId;

        _logger.LogInformation("Cart has been created for User {UserId}", currentUserId);

        return newCart;
    }

    public async Task<CartDTO> GetOrCreatePendingCartAsync()
    {
        var currentUserId = _currentUserService.GetUserId();

        var pendingCart = await _cartRepository.GetLastPendingCartAsync(currentUserId);
        if(pendingCart is not null)
        {
            _logger.LogInformation("Cart with Id {CartId} has been retrieved for User {UserId}", pendingCart.Id, currentUserId);
            
            return pendingCart;
        }

        _logger.LogInformation("No pending cart has been found for User {UserId}", currentUserId);
        return await CreateNewAsync();
    }

    public async Task AddItemAsync(CartItemDTO newCartItem)
    {
        // validations for cartitem here. add is room booked etc.. (so important.)  
        await _cartItemValidator.ValidateAndThrowAsync(newCartItem);

        var pendingCart = await GetOrCreatePendingCartAsync();
        newCartItem.CartId = pendingCart.Id;
        newCartItem.CreationDate = DateTime.UtcNow;
        var itemPrice = await _roomService
            .GetBookingPriceForRoom(
                newCartItem.RoomId,
                newCartItem.CheckInDate,
                newCartItem.CheckOutDate);

        newCartItem.Price = itemPrice;
            
        await _cartRepository.AddItemAsync(newCartItem);
        
         _logger.LogInformation("Added Room {RoomId} to Cart {CartId} for User {UserId}", newCartItem.RoomId, pendingCart.Id, pendingCart.UserId);
    }

    public async Task DeleteItemAsync(Guid cartItemId)
    {
        var currentUserId = _currentUserService.GetUserId();
        await ValidateCartItemIdAsync(cartItemId);
        await ValidateOwnershipAsync(cartItemId, currentUserId);

        await _cartRepository.DeleteItemAsync(cartItemId);

        _logger.LogInformation("Deleted Room {RoomId} from Cart {CartId} for User {UserId}", cartItemId, cartItemId, _currentUserService.GetUserId());
    }

    private async Task ValidateCartItemIdAsync(Guid cartItemId)
    {
        if(!await _cartItemRepository.ExistsAsync(cartItemId))
        {
            throw new KeyNotFoundException($"CartItem with ID {cartItemId} does not exist.");
        }
    }

    private async Task ValidateOwnershipAsync(Guid cartItemId, Guid userId)
    {
        var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
        if (cartItem is null)
        {
            throw new KeyNotFoundException($"CartItem with ID {cartItemId} does not exist.");
        }

        var cart = await _cartRepository.GetLastPendingCartAsync(userId);
        if (cart is null || cartItem.CartId != cart.Id)
        {
            throw new KeyNotFoundException($"CartItem with ID {cartItemId} does not belong to the current user.");
        }
    }
    
    public async Task CheckOutAsync()
    {
        var cart = await _cartRepository
            .GetLastPendingCartAsync(_currentUserService.GetUserId());
        
        bool IsInvalidCart = cart is null || cart.Items is null || cart.Items.Count == 0;

        if(IsInvalidCart)
        {
            throw new InvalidOperationException("No pending cart or cart is empty."); // do proper fluentvalidation here.
        }

        try {
            await _roomBookingService.AddAsync(cart); // this throws exception if booking is not valid.

            cart.Status = BookingStatus.Confirmed;
            cart.CheckOutDate = DateTime.UtcNow;

            await _cartRepository.UpdateAsync(cart);

            _logger.LogInformation("Cart {CartId} successfully checked out for User {UserId}", cart.Id, cart.UserId);
        } 
        catch (Exception ex)
        {
            cart.Status = BookingStatus.Cancelled;
            cart.ModificationDate = DateTime.UtcNow;
            await _cartRepository.UpdateAsync(cart);

            _logger.LogError(ex, "Checkout failed for Cart {CartId} for User {UserId}. Cart marked as Cancelled.", cart.Id, cart.UserId);

            throw;
        }

    }

    public async Task<IEnumerable<CartItemDTO>> GetCartItemsAsync(
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var currentUserId = _currentUserService.GetUserId();
        var cart = await _cartRepository.GetLastPendingCartAsync(currentUserId);
        if(cart is null)
        {
            return [];
        }

        var cartItems = await _cartRepository.GetAllCartItemsAsync(cart.Id, pagination.PageNumber, pagination.PageSize);

        _logger.LogInformation("Cart with Id {CartId} has been retrieved for User {UserId}", cart.Id, currentUserId);

        return cartItems;
    }

    public async Task<CartUserResponseDTO> GetCurrentCartDetailsAsync()
    {
        var currentUserId = _currentUserService.GetUserId();
        var cart = await _cartRepository
            .GetCartDetailsByUserIdAsync(currentUserId);


        if(cart is null)
            return new CartUserResponseDTO();
        
        var cartItems = cart.Items;
        decimal x = 0;
        foreach(var item in cartItems)
        {
            var price = await _roomService.GetBookingPriceForRoom(item.RoomId, item.CheckInDate, item.CheckOutDate);
            item.Price = price;
            x+= price;
        }
        cart.TotalPrice = x;
        // await _cartRepository.UpdateAsync(cart);

        _logger.LogInformation("Cart with Id {CartId} has been retrieved for User {UserId}", cart.Id, currentUserId);
        return cart;
    }
}