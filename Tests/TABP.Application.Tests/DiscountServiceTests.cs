using AutoFixture;
using FluentValidation;
using Moq;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Models.Discount;
using TABP.Application.Services;
using TABP.Domain.Models.Pagination;
using Microsoft.Extensions.Logging;
using TABP.Domain.Models.Discount.Sort;
using FluentAssertions;
using TABP.Domain.Exceptions;

namespace TABP.Application.Tests;

public class DiscountServiceTests
{
    private readonly Mock<IDiscountRepository> _mockDiscountRepo;
    private readonly Mock<IValidator<DiscountDTO>> _mockDiscountValidator;
    private readonly Mock<IValidator<PaginationDTO>> _mockPaginationValidator;
    private readonly Mock<ILogger<DiscountService>> _mockLogger;
    private readonly Mock<IValidator<DiscountSortQuery>> _mockSortValidator;
    private readonly Mock<ICurrentUserService> _mockCurrentUserService;
    private readonly DiscountService _sut;
    private readonly Fixture _fixture;

    public DiscountServiceTests()
    {
        _mockDiscountRepo = new Mock<IDiscountRepository>();
        _mockDiscountValidator = new Mock<IValidator<DiscountDTO>>();
        _mockPaginationValidator = new Mock<IValidator<PaginationDTO>>();
        _mockLogger = new Mock<ILogger<DiscountService>>();
        _mockSortValidator = new Mock<IValidator<DiscountSortQuery>>();
        _mockCurrentUserService = new Mock<ICurrentUserService>();

        _fixture = new Fixture();

        _mockDiscountValidator
            .Setup(v => v.ValidateAsync(It.IsAny<DiscountDTO>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _sut = new DiscountService(
            _mockDiscountRepo.Object,
            _mockLogger.Object,
            _mockDiscountValidator.Object,
            _mockPaginationValidator.Object,
            _mockCurrentUserService.Object,
            _mockSortValidator.Object);
    }

    [Fact]
    public async Task AddAsync_WhenValid_ShouldCallRepository()
    {
        // Arrange
        var discount = _fixture.Create<DiscountDTO>();

        // Act
        await _sut.AddAsync(discount);

        // Assert
        _mockDiscountRepo.Verify(
            repo => repo.AddAsync(discount),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenDoesNotExist_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var discountId = Guid.NewGuid();
        _mockDiscountRepo
            .Setup(repo => repo.ExistsAsync(discountId))
            .ReturnsAsync(false);

        // Act & Assert
        await _sut.Invoking(s => s.DeleteAsync(discountId))
            .Should()
            .ThrowAsync<EntityNotFoundException>();
        _mockDiscountRepo.Verify(
            repo => repo.DeleteAsync(It.IsAny<Guid>()),
            Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_WhenExists_ShouldCallRepository()
    {
        // Arrange
        var discountId = Guid.NewGuid();

        _mockDiscountRepo
            .Setup(repo => repo.ExistsAsync(discountId))
            .ReturnsAsync(true);

        // Act
        await _sut.DeleteAsync(discountId);

        // Assert
        _mockDiscountRepo.Verify(
            repo => repo.DeleteAsync(It.IsAny<Guid>()),
            Times.Once);
    }
}