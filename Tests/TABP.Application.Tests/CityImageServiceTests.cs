using AutoFixture;
using FluentAssertions;
using Moq;
using TABP.Application.Services.City;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using FluentValidation;
using TABP.Domain.Models.Image;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Constants.Image;
using TABP.Domain.Exceptions;

namespace TABP.Application.Tests;
public class CityImageServiceTests
{
    private readonly Mock<IImageService> _mockImageService;
    private readonly Mock<ILogger<CityImageService>> _loggerMock;
    private readonly Mock<IValidator<ImageSizeDTO>> _mockImageSizeVaidator;
    private readonly Mock<IValidator<IEnumerable<Image>>> _mockImageVaidator;
    private readonly Mock<IImageRepository> _mockImageRepo;
    private readonly CityImageService _sut;
    private readonly Fixture _fixture;

    public CityImageServiceTests()
    {
        _mockImageSizeVaidator = new Mock<IValidator<ImageSizeDTO>>();
        _mockImageVaidator = new Mock<IValidator<IEnumerable<Image>>>();
        _mockImageRepo = new Mock<IImageRepository>();
        _mockImageService = new Mock<IImageService>();
        _loggerMock = new Mock<ILogger<CityImageService>>();
        _fixture = new Fixture();

        _sut = new CityImageService(
            _mockImageService.Object,
            _loggerMock.Object
        );

        _mockImageVaidator
            .Setup(validate => validate.ValidateAsync(
                It.IsAny<IEnumerable<Image>>(),
                default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mockImageSizeVaidator
            .Setup(validate => validate.ValidateAsync(
                It.IsAny<ImageSizeDTO>(),
                default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());
    }

    [Fact]
    public async Task AddImagesAsync_ShouldThrowEntityImageLimitExceededException_WhenImageCountIsMoreThanConstant()
    {
        var cityId = new Guid();
        var images = _fixture.Build<List<Image>>()
            .Create();
        // Arrange
        _mockImageService
            .Setup(service => service.GetCountAsync(cityId))
            .ReturnsAsync(ImageConstants.MaxNumberOfImages+1);

        _mockImageService
            .Setup(service => service.ExistsAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        // Act & Assert
        await _sut.Invoking(service => service.AddImagesAsync(cityId, images))
            .Should()
            .ThrowAsync<EntityImageLimitExceededException>();

        _mockImageService.Verify(
            service => service.AddAsync(cityId, images), Times.Never);
    }
}
