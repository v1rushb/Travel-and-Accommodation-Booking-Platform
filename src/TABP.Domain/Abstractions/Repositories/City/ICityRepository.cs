using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.City;
using TABP.Domain.Models.City.Search;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository to manage <see cref="City"/> entities.
/// This interface provides asynchronous operations for creating, retrieving, updating, and deleting city data,
/// as well as searching and checking for the existence of cities based on various criteria.
/// </summary>
public interface ICityRepository
{
    /// <summary>
    /// Asynchronously adds a new city to the repository.
    /// </summary>
    /// <param name="newCity">A <see cref="CityDTO"/> containing the data for the new city.</param>
    /// <returns>A <see cref="Task{Guid}"/> representing the asynchronous operation, and upon completion,
    /// returns the unique identifier of the newly added city.
    /// </returns>
    Task<Guid> AddAsync(CityDTO newCity);
    
    /// <summary>
    /// Asynchronously checks if a city with the specified ID exists in the repository.
    /// </summary>
    /// <param name="Id">The unique identifier of the city to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a city with the given ID exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);


    /// <summary>
    /// Asynchronously retrieves a city from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the city to retrieve.</param>
    /// <returns>A <see cref="Task{CityDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="CityDTO"/>.
    /// </returns>
    Task<CityDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Asynchronously deletes a city from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the city to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid Id);



    /// <summary>
    /// Asynchronously updates an existing city in the repository.
    /// </summary>
    /// <param name="updatedCity">A <see cref="CityDTO"/> containing the updated data for the city.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(CityDTO updatedCity);
    

    /// <summary>
    /// Searches for cities in the repository based on a predicate, with pagination and optional sorting.
    /// </summary>
    /// <param name="predicate">An <see cref="Expression{Func{City, bool}}"/> that defines the search conditions.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of cities per page.</param>
    /// <param name="orderByDelegate">An optional <see cref="Func{IQueryable{City}, IOrderedQueryable{City}}"/> delegate to specify the sorting order.</param>
    /// <returns>A <see cref="Task{IEnumerable{CitySearchResponseDTO}}"/> representing the asynchronous operation, and upon completion, 
    /// returns a collection of <see cref="CitySearchResponseDTO"/> that match the search criteria.
    /// </returns>
    Task<IEnumerable<CitySearchResponseDTO>> SearchAsync(
        Expression<Func<City, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<City>, IOrderedQueryable<City>> orderByDelegate = null);

    /// <summary>
    /// Asynchronously checks if a city with the specified name and country already exists in the repository.
    /// </summary>
    /// <param name="name">The name of the city to check.</param>
    /// <param name="country">The country name of the city to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a city with the given name and country exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsByNameAndCountryAsync(string name, string country);
}