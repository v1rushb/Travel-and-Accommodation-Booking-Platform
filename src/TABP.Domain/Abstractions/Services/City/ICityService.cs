using TABP.Domain.Models.City;

namespace TABP.Domain.Abstractions.Services.City;

/// <summary>
/// Defines core operations for managing city data.
/// for city entities, along with a method to check for the existence of a city.
/// </summary>
public interface ICityService
{
    /// <summary>
    /// Retrieves a city by its unique <see cref="Guid"/> identifier.
    /// This method fetches the details of a city based on its ID, providing access
    /// to all stored information about the city.
    /// </summary>
    /// <param name="Id">
    /// The unique <see cref="Guid"/> identifier of the city to retrieve.
    /// This ID is used to locate the specific city record in the data store.
    /// </param>
    /// <returns>
    /// <see cref="CityDTO"/> representing the city details if found.
    /// Returns null or throws an exception if the city with the given ID does not exist, depending on implementation.
    /// </returns>
    Task<CityDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Adds a new city to the system.
    /// This method creates a new city record using the provided city data.
    /// It handles the validation and persistence of the new city information.
    /// </summary>
    /// <param name="newCity">
    /// <see cref="CityDTO"/> containing details of the city to be added.
    /// This DTO encapsulates all necessary information for creating a new city record.
    /// </param>
    Task AddAsync(CityDTO newCity);

    /// <summary>
    /// Deletes a city from the system by its unique <see cref="Guid"/> identifier.
    /// This method removes a city record from the data store. It should also handle
    /// any associated data, such as related images or hotel information, to maintain data integrity.
    /// </summary>
    /// <param name="Id">
    /// The unique <see cref="Guid"/> identifier of the city to be deleted.
    /// This ID specifies which city record should be removed from the system.
    /// </param>
    Task DeleteAsync(Guid Id);

    /// <summary>
    /// Updates an existing city's information.
    /// This method modifies the details of a city record using the provided updated city data.
    /// It is used to apply changes to city attributes, ensuring that the city information is current.
    /// </summary>
    /// <param name="updatedCity">
    /// <see cref="CityDTO"/> containing updated details for the city.
    /// This DTO should include the ID of the city to be updated and the new values for its attributes.
    /// </param>
    Task UpdateAsync(CityDTO updatedCity);

    /// <summary>
    /// Checks if a city exists in the system by its unique <see cref="Guid"/> identifier.
    /// This method is used to verify the presence of a city record before performing operations
    /// that depend on its existence, such as retrieval, update, or deletion.
    /// </summary>
    /// <param name="Id">
    /// The unique <see cref="Guid"/> identifier of the city to check for existence.
    /// The method queries the data store to determine if a city with this ID is present.
    /// </param>
    /// <returns>
    /// <c>true</c> if a city with the given ID exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);
}