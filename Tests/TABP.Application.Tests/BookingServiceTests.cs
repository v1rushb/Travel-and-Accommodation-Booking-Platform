using FluentAssertions;
using FluentValidation;
using Moq;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Models.RoomBooking;
using TABP.Domain.Enums;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Application.Services.Booking;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.CartItem;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.Discount;
using AutoFixture;
using TABP.Domain.Exceptions;
using TABP.Domain.Abstractions.Utilities.Injectable;

namespace TABP.Application.Tests;
public class BookingServiceTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IRoomBookingRepository> _mockBookingRepo;
    private readonly Mock<IRoomService> _mockRoomService;
    private readonly Mock<IDiscountRepository> _mockDiscountRepo;
    private readonly Mock<ICurrentUserService> _mockCurrentUser;
    private readonly Mock<IValidator<RoomBookingDTO>> _mockValidator;
    private readonly Mock<IRoomBookingEmailService> _mockBookingEmailService;
    private readonly IRoomBookingService _sut;

    public BookingServiceTests()
    {
        _fixture = new Fixture();
        _mockBookingRepo = new Mock<IRoomBookingRepository>();
        _mockRoomService = new Mock<IRoomService>();
        _mockDiscountRepo = new Mock<IDiscountRepository>();
        _mockCurrentUser = new Mock<ICurrentUserService>();
        _mockValidator = new Mock<IValidator<RoomBookingDTO>>();
        _mockBookingEmailService = new Mock<IRoomBookingEmailService>();



        _mockValidator
            .Setup(validate => validate.ValidateAsync(
                It.IsAny<RoomBookingDTO>(),
                default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mockCurrentUser
            .Setup(currentUser => currentUser.GetUserId())
            .Returns(Guid.NewGuid());

        _sut = new RoomBookingService(
            _mockBookingRepo.Object,
            _mockDiscountRepo.Object,
            _mockRoomService.Object,
            _mockValidator.Object,
            _mockBookingEmailService.Object
        );
    }

    [Fact]
    public async Task AddAsync_WhenCartIsProvided_ShouldAddAtleastOneBooking()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var hotelId = Guid.NewGuid();

        var cart = _fixture.Build<CartDTO>()
            .With(c => c.UserId, userId)
            .With(c => c.Items, new List<CartItemDTO>
            {
                _fixture.Build<CartItemDTO>()
                    .With(item => item.RoomId, roomId)
                    .With(item => item.CheckInDate, DateTime.UtcNow.AddDays(1))
                    .With(item => item.CheckOutDate, DateTime.UtcNow.AddDays(3))
                    .Create()
            })
            .Create();


        var mockRoom = _fixture.Build<RoomDTO>()
            .With(room => room.Id, roomId)
            .With(room => room.HotelId, hotelId)
            .With(room => room.Type, RoomType.Luxury)
            .With(room => room.PricePerNight, 100M)
            .Create();

        var mockDiscount = _fixture.Build<DiscountDTO>()
            .With(discount => discount.AmountPercentage, 10M)
            .Create();

        _mockRoomService
            .Setup(s => s.GetByIdAsync(roomId))
            .ReturnsAsync(mockRoom);

        _mockDiscountRepo
            .Setup(d => d.GetHighestDiscountActiveForHotelRoomTypeAsync(hotelId, RoomType.Luxury))
            .ReturnsAsync(mockDiscount);

        // Act
        await _sut.AddAsync(cart);

        // Assert
        _mockBookingRepo.Verify(
            repo => repo.AddAsync(It.IsAny<List<RoomBookingDTO>>()),
            Times.AtLeastOnce);
    }



    [Fact]
    public async Task GetByIdAsync_WhenBookingDoesNotExist_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var nonExistingBookingId = Guid.NewGuid();

        _mockBookingRepo
            .Setup(repo => repo.ExistsAsync(nonExistingBookingId))
            .ReturnsAsync(false);

        // Act & Assert
        await _sut.Invoking(s => s.GetByIdAsync(nonExistingBookingId))
            .Should()
            .ThrowAsync<EntityNotFoundException>();
    }
}