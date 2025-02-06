using AutoFixture;
using FluentValidation;
using Moq;
using SixLabors.ImageSharp;
using TABP.Application.Services;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Models.Image;
using FluentAssertions;
using TABP.Domain.Exceptions;

namespace TABP.Application.Tests;
public class ImageServiceTests
{
    private readonly Mock<IImageRepository> _mockImageRepository;
    private readonly Mock<IValidator<ImageSizeDTO>> _mockSizeValidator;
    private readonly Mock<IValidator<IEnumerable<Image>>> _mockImageValidator;
    private readonly ImageService _sut;
    private readonly Fixture _fixture;

    public ImageServiceTests()
    {
        _mockImageRepository = new Mock<IImageRepository>();
        _mockSizeValidator = new Mock<IValidator<ImageSizeDTO>>();
        _mockImageValidator = new Mock<IValidator<IEnumerable<Image>>>();

        _fixture = new Fixture();

        _mockSizeValidator
            .Setup(validate => validate.ValidateAsync(
                It.IsAny<ImageSizeDTO>(),
                default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());
        
        _mockImageValidator
            .Setup(validate => validate.ValidateAsync(
                It.IsAny<IEnumerable<Image>>(),
                default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _sut = new ImageService(
            _mockImageRepository.Object,
            _mockSizeValidator.Object,
            _mockImageValidator.Object
        );
    }

    [Fact]
    public async Task AddAsync_WhenImagesAreValid_ShouldCallRepository()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var images = _fixture.Build<List<Image>>()
            .Create();

        // Act
        await _sut.AddAsync(entityId, images);

        // Assert
        _mockImageRepository.Verify(repo => 
            repo.AddAsync(entityId, images),
            Times.Once);
    }

    [Fact]
    public async Task GetAsync_WithValidIdAndSize_ShouldReturnStream()
    {
        // Arrange
        var imageId = Guid.NewGuid();
        var size = _fixture.Build<ImageSizeDTO>()
            .Create();
            
        var mockStream = new MemoryStream();

        _mockImageRepository
            .Setup(repo => repo.ExistsAsync(imageId))
            .ReturnsAsync(true);

        _mockImageRepository
            .Setup(repo => repo.GetAsync(imageId, size))
            .ReturnsAsync(mockStream);

        // Act
        var result = await _sut.GetAsync(imageId, size);

        // Assert
        result.Should().BeSameAs(mockStream);

        _mockImageRepository.Verify(
            repo => repo.GetAsync(imageId, size),
            Times.Once);
    }

    [Fact]
    public async Task GetAsync_WhenIdNotFoundShouldThrow_EntityNotFoundException()
    {
        // Arrange
        var imageId = Guid.NewGuid();
        var size = _fixture.Build<ImageSizeDTO>()
            .Create();

        _mockImageRepository
            .Setup(repo => repo.ExistsAsync(imageId))
            .ReturnsAsync(false);

        // Act & Assert
        await _sut.Invoking(service => service.GetAsync(imageId, size))
            .Should()
            .ThrowAsync<EntityNotFoundException>();

        _mockImageRepository.Verify(repo => 
            repo.GetAsync(
                It.IsAny<Guid>(),
                It.IsAny<ImageSizeDTO>()), 
                Times.Never
            );
    }

    [Fact]
    public async Task GetCountAsync_WhenCalled_ShouldReturnRepositoryCount()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        _mockImageRepository
            .Setup(repo => repo.GetCountAsync(entityId))
            .ReturnsAsync(5);

        // Act
        var result = await _sut.GetCountAsync(entityId);

        // Assert
        Assert.Equal(5, result);
        _mockImageRepository.Verify(repo => 
            repo.GetCountAsync(entityId), 
            Times.Once);
    }

    [Fact]
    public async Task GetIdsForEntityAsync_WhenCalled_ShouldReturnRepositoryIds()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var expectedIds = _fixture.Build<List<Guid>>()
            .Create();

        _mockImageRepository
            .Setup(repo => repo.GetIdsForEntityAsync(entityId))
            .ReturnsAsync(expectedIds);

        // Act
        var result = await _sut.GetIdsForEntityAsync(entityId);

        // Assert
        Assert.Equal(expectedIds, result);
        _mockImageRepository.Verify(repo => 
            repo.GetIdsForEntityAsync(entityId), 
            Times.Once);
    }
}