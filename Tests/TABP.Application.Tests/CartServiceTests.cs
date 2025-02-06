using AutoFixture;
using FluentValidation;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.CartItem;
using TABP.Domain.Enums;
using TABP.Application.Services.Cart;
using TABP.Domain.Models.Pagination;
using AutoMapper;
using TABP.Domain.Exceptions;

namespace TABP.Application.Tests;
public class CartServiceTests
{
    private readonly Fixture _fixture;
    private readonly Mock<ICartRepository> _mockCartRepo;
    private readonly Mock<IRoomBookingService> _mockBookingService;
    private readonly Mock<ICurrentUserService> _mockCurrentUser;
    private readonly Mock<ICartItemRepository> _mockCartItemRepo;
    private readonly Mock<ILogger<CartService>> _mockLogger;
    private readonly Mock<IValidator<CartItemDTO>> _mockCartItemValidator;
    private readonly Mock<IValidator<PaginationDTO>> _mockPaginationValidator;
    private readonly Mock<IRoomService> _mockRoomService;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;


    private readonly CartService _sut;

    public CartServiceTests()
    {
        _fixture = new Fixture();
        _mockCartRepo = new Mock<ICartRepository>();
        _mockBookingService = new Mock<IRoomBookingService>();
        _mockCurrentUser = new Mock<ICurrentUserService>();
        _mockCartItemRepo = new Mock<ICartItemRepository>();
        _mockLogger = new Mock<ILogger<CartService>>();
        _mockCartItemValidator = new Mock<IValidator<CartItemDTO>>();
        _mockPaginationValidator = new Mock<IValidator<PaginationDTO>>();
        _mockRoomService = new Mock<IRoomService>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();

        _mockCartItemValidator
            .Setup(validate => validate.ValidateAsync(
                    It.IsAny<CartItemDTO>(),
                    default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mockCartRepo
            .Setup(repo => repo.GetLastPendingCartAsync(It.IsAny<Guid>()))
            .ReturnsAsync((CartDTO)null);

        _mockCurrentUser
            .Setup(currentUser => currentUser.GetUserId())
            .Returns(Guid.NewGuid());

        _sut = new CartService(
            _mockCartRepo.Object,
            _mockBookingService.Object,
            _mockCartItemRepo.Object,
            _mockCurrentUser.Object,
            _mockLogger.Object,
            _mockCartItemValidator.Object,
            _mockPaginationValidator.Object,
            _mockRoomService.Object,
            _mockUnitOfWork.Object,
            _mockMapper.Object
        );
    }

    [Fact]
    public async Task GetOrCreatePendingCartAsync_NoExistingPendingCart_ShouldCreateNewCart()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockCurrentUser
            .Setup(currentUser => currentUser.GetUserId())
            .Returns(userId);

        // Act
        var result = await _sut.GetOrCreatePendingCartAsync();


        
        // Assert
        result.Status.Should().Be(BookingStatus.Pending);

        _mockCartRepo.Verify(repo => 
            repo.CreateAsync(It.IsAny<CartDTO>()), Times.Once);
    }

    [Fact]
    public async Task AddItemAsync_ShouldAddItemToCart()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockCurrentUser
            .Setup(currentUser => currentUser.GetUserId())
            .Returns(userId);

        var pendingCart = new CartDTO
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Status = BookingStatus.Pending
        };

        _mockCartRepo
            .Setup(repo => repo.GetLastPendingCartAsync(userId))
            .ReturnsAsync(pendingCart);

        var cartItem = _fixture.Build<CartItemDTO>()
            .With(cartItem => cartItem.CheckInDate, DateTime.UtcNow.AddDays(1))
            .With(cartItem => cartItem.CheckOutDate, DateTime.UtcNow.AddDays(2))
            .Create();

        // Act
        await _sut.AddItemAsync(cartItem);

        // Assert
        _mockCartRepo.Verify(repo => repo.AddItemAsync(It.IsAny<CartItemDTO>()), Times.Once);
    }

    [Fact]
    public async Task CheckOutAsync_WhenNoPendingCart_ShouldThrowInvalidOperationException()
    {
        // Act & Assert
        await _sut.Invoking(service => service.CheckOutAsync())
            .Should()
            .ThrowAsync<EmptyCartException>();
    }

    [Fact]
    public async Task CheckOutAsync_WhenValidCart_ShouldCallAddBooking()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var pendingCart = _fixture.Build<CartDTO>()
            .With(cart => cart.Status, BookingStatus.Pending)
            .With(cart => cart.Items, new List<CartItemDTO>
            {
                _fixture.Create<CartItemDTO>()
            })
            .With(cart => cart.UserId, userId)
            .Create();

        _mockCurrentUser
            .Setup(currentUser => currentUser.GetUserId())
            .Returns(userId);

        _mockCartRepo
            .Setup(repo => repo.GetLastPendingCartAsync(userId))
            .ReturnsAsync(pendingCart);

        // Act
        await _sut.CheckOutAsync();

        // Assert
        _mockBookingService.Verify(bookingService => 
            bookingService.AddAsync(pendingCart), Times.Once);
        _mockCartRepo.Verify(repo => 
            repo.UpdateAsync(It.Is<CartDTO>(cart => 
                cart.Status == BookingStatus.Confirmed)), Times.Once);
    }

    [Fact]
    public async Task DeleteItemAsync_WhenUserDoesNotOwnItem_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var cartItemId = Guid.NewGuid();
        var currentUserId = Guid.NewGuid();

        _mockCurrentUser
            .Setup(currentUser => currentUser.GetUserId())
            .Returns(currentUserId);

        _mockCartItemRepo
            .Setup(repo => repo.ExistsAsync(cartItemId))
            .ReturnsAsync(true);

        _mockCartRepo
            .Setup(repo => repo.GetLastPendingCartAsync(currentUserId))
            .ReturnsAsync(new CartDTO
            {
                Id = Guid.NewGuid(),
                UserId = currentUserId, 
                Status = BookingStatus.Pending
            });

        _mockCartItemRepo
            .Setup(repo => repo.GetByIdAsync(cartItemId))
            .ReturnsAsync(new CartItemDTO
            {
                CartId = Guid.NewGuid(),
            });

        // Act & Assert
        await _sut.Invoking(service => service.DeleteItemAsync(cartItemId))
            .Should()
            .ThrowAsync<EntityNotFoundException>();

        _mockCartRepo.Verify(
            repo => repo.DeleteItemAsync(
                It.IsAny<Guid>()),
                Times.Never
            );

    }
}
