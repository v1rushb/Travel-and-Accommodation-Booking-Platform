using AutoFixture;
using FluentValidation;
using Moq;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Repositories.Review;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.HotelReview;
using TABP.Application.Services.Review;
using TABP.Domain.Models.Hotel;
using FluentAssertions;
using TABP.Domain.Exceptions;

namespace TABP.Application.Tests;
public class HotelReviewServiceTests
{
    private readonly Mock<IHotelReviewRepository> _mockReviewRepo;
    private readonly Mock<IValidator<HotelReviewDTO>> _mockReviewValidator;
    private readonly Mock<ICurrentUserService> _mockCurrentUserService;
    private readonly Mock<IHotelRepository> _mockHotelRepo;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly HotelReviewService _sut;
    private readonly Fixture _fixture;

    public HotelReviewServiceTests()
    {
        _mockReviewRepo = new Mock<IHotelReviewRepository>();
        _mockReviewValidator = new Mock<IValidator<HotelReviewDTO>>();
        _mockCurrentUserService = new Mock<ICurrentUserService>();
        _mockHotelRepo = new Mock<IHotelRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _fixture = new Fixture();

        _mockReviewValidator
            .Setup(validate => validate.ValidateAsync(
                It.IsAny<HotelReviewDTO>(),
                default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _sut = new HotelReviewService(
            _mockReviewRepo.Object,
            _mockReviewValidator.Object,
            _mockCurrentUserService.Object,
            _mockHotelRepo.Object,
            _mockUnitOfWork.Object
        );
    }

    [Fact]
    public async Task AddAsync_WhenValid_ShouldCallRepoAndUpdateHotelRating()
    {
        // Arrange
        var hotelId = new Guid();
        var userId = new Guid();

        _mockCurrentUserService
            .Setup(currentUser => currentUser.GetUserId())
            .Returns(userId);
        
        var hotel = _fixture.Build<HotelDTO>()
            .With(hotel => hotel.Id, hotelId)
            .Create();

        _mockHotelRepo
            .Setup(repo => repo.GetByIdAsync(hotelId))
            .ReturnsAsync(hotel);

        var review = _fixture.Build<HotelReviewDTO>()
            .With(review => review.HotelId, hotelId)
            .With(review => review.Rating, 1)
            .Create();

        var oldHotelRating = hotel.StarRating;

        // Act
        await _sut.AddAsync(review);

        // Assert
        _mockReviewRepo.Verify(
            repo => repo.AddAsync(review),
            Times.Once);

        _mockUnitOfWork.Verify(
            uow => uow.SaveChangesAsync(),
            Times.Once);

        hotel.StarRating.Should().NotBe(oldHotelRating);
    }

    [Fact]
    public async Task DeleteAsync_WhenUserIsOwner_ShouldDeleteAndUpdateHotelRating()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var hotelId = Guid.NewGuid();

        var review = _fixture.Build<HotelReviewDTO>()
            .With(review => review.Id, reviewId)
            .With(review => review.HotelId, hotelId)
            .With(review => review.Rating, 1)
            .Create();

        var hotel = _fixture.Build<HotelDTO>()
            .With(hotel => hotel.Id, hotelId)
            .Create();

        _mockReviewRepo
            .Setup(repo => repo.ExistsAsync(reviewId, It.IsAny<Guid?>()))
            .ReturnsAsync(true);

        _mockHotelRepo
            .Setup(repo => repo.GetByIdAsync(hotelId))
            .ReturnsAsync(hotel);

        _mockReviewRepo
            .Setup(repo => repo.GetByIdAsync(reviewId))
            .ReturnsAsync(review);

        _mockCurrentUserService
            .Setup(currnetUser => currnetUser.GetUserId())
            .Returns(userId);

        // Act
        await _sut.DeleteAsync(reviewId);

        // Assert
        _mockReviewRepo.Verify(
            repo => repo.DeleteAsync(reviewId),
            Times.Once);

        _mockUnitOfWork.Verify(
            uow => uow.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenUserDoesNotOwnReview_ShouldThrow()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        var anotherUserId = Guid.NewGuid();
        var currentUser = Guid.NewGuid();

        var review = _fixture.Build<HotelReviewDTO>()
            .With(review => review.Id, reviewId)
            .With(review => review.UserId, anotherUserId)
            .Create();

        _mockReviewRepo
            .Setup(repo => repo.ExistsAsync(reviewId, anotherUserId))
            .ReturnsAsync(true);

        // Act & Assert
        await _sut.Invoking(s => s.DeleteAsync(reviewId))
                .Should()
                .ThrowAsync<EntityNotFoundException>();

        _mockReviewRepo.Verify(
            repo => repo.DeleteAsync(It.IsAny<Guid>()),
            Times.Never);
    }
}