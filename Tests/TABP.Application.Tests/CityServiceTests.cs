using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TABP.Application.Services.City;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Models.City;
using TABP.Domain.Exceptions;

namespace TABP.Application.Tests
{
    public class CityServiceTests
    {
        private readonly Mock<ICityRepository> _cityRepositoryMock;
        private readonly Mock<IValidator<CityDTO>> _cityValidatorMock;
        private readonly CityService _sut;
        private readonly Fixture _fixture;

        public CityServiceTests()
        {
            _cityRepositoryMock = new Mock<ICityRepository>();
            _cityValidatorMock = new Mock<IValidator<CityDTO>>();
            _fixture = new Fixture();

            _sut = new CityService(
                _cityRepositoryMock.Object,
                _cityValidatorMock.Object
            );
        }

        [Fact]
        public async Task AddAsync_ShouldSetDatesAndCallRepositoryWhen_Valid()
        {
            // Arrange
            var newCity = _fixture.Create<CityDTO>();
            var cityValidatorMock = new Mock<IValidator<CityDTO>>();

            _cityValidatorMock
                .Setup(val => val.Validate(newCity))
                .Returns(new ValidationResult());

            // Act
            await _sut.AddAsync(newCity);

            // Assert
            newCity.CreationDate.Should().NotBe(default);
            newCity.ModificationDate.Should().NotBe(default);

            _cityRepositoryMock
                .Verify(repo => repo.AddAsync(newCity), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowEntityNotFoundWhen_CityNotFound() 
        {
            // Arrange
            var cityId = Guid.NewGuid();
            
            _cityRepositoryMock
                .Setup(repo =>
                    repo.ExistsAsync(cityId))
                    .ReturnsAsync(false);

            // Act && Assert
            await _sut.Invoking(service => service.GetByIdAsync(cityId))
                .Should()
                .ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCityWhen_Exists()
        {
            // Arrange
            var cityId = Guid.NewGuid();
            var existingCity = _fixture.Build<CityDTO>()
                .With(city => city.Id, cityId)
                .Create();

            _cityRepositoryMock
                .Setup(repo => repo.ExistsAsync(cityId))
                .ReturnsAsync(true);

            _cityRepositoryMock
                .Setup(repo => repo.GetByIdAsync(cityId))
                .ReturnsAsync(existingCity);

            // Act
            var result = await _sut.GetByIdAsync(cityId);

            // Assert
            result.Should().Be(existingCity);
        }


        [Fact]
        public async Task UpdateAsync_ShouldThrowEntityNotFoundWhen_CityNotFound()
        {
            // Arrange
            var updatedCity = _fixture.Create<CityDTO>();
            _cityRepositoryMock
                .Setup(repo => repo.ExistsAsync(updatedCity.Id))
                .ReturnsAsync(false);

            // Act && Assert
            await _sut.Invoking(service => service.UpdateAsync(updatedCity))
                .Should()
                .ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task UpdateAsync_ShouldSetModificationDateAndCallRepository_When_Exists()
        {
            // Arrange
            var existingCity = _fixture.Build<CityDTO>()
                .With(city => city.Id, Guid.NewGuid())
                .Create();

            _cityRepositoryMock
                .Setup(repo => repo.ExistsAsync(existingCity.Id))
                .ReturnsAsync(true);

            // Act
            await _sut.UpdateAsync(existingCity);

            // Assert
            existingCity.ModificationDate
                .Should().NotBe(default);

            _cityRepositoryMock.Verify(repo => 
                    repo.UpdateAsync(existingCity), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowEntityNotFoundWhen_CityNotFound()
        {
            // Arrange
            var cityId = Guid.NewGuid();
            _cityRepositoryMock
                .Setup(repo => repo.ExistsAsync(cityId))
                .ReturnsAsync(false);

            // Act && Assert
            await _sut.Invoking(service => service.DeleteAsync(cityId))
                .Should()
                .ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryWhen_CityExists()
        {
            // Arrange
            var cityId = Guid.NewGuid();
            _cityRepositoryMock
                .Setup(repo => repo.ExistsAsync(cityId))
                .ReturnsAsync(true);

            // Act
            await _sut.DeleteAsync(cityId);

            // Assert
            _cityRepositoryMock.Verify(repo => 
                repo.DeleteAsync(cityId), Times.Once);
        }
    }
}
