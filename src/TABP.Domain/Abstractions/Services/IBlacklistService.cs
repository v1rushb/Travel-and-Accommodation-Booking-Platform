namespace TABP.Domain.Abstractions.Services;


/// <summary>
/// Provides operations for managing token blacklisting.
/// This service handles adding tokens to a blacklist and verifying if a token has been blacklisted.
/// </summary>
public interface IBlacklistService
{
    /// <summary>
    /// Adds the specified token to the blacklist for a given duration.
    /// </summary>
    /// <param name="token">The token to be added to the blacklist.</param>
    /// <param name="expiration">
    /// The duration after which the token should no longer be considered blacklisted.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the token is null or empty.
    /// </exception>
    /// <exception cref="Exceptions.CacheException">
    /// Thrown when an error occurs while adding the token to the blacklist.
    /// </exception>
    Task AddToBlacklistAsync(
        string token,
        TimeSpan expiration);

    /// <summary>
    /// Determines whether the specified token is currently blacklisted.
    /// </summary>
    /// <param name="token">The token to check for blacklisting.</param>
    /// <returns>
    /// <c>true</c> if the token is blacklisted; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="Exceptions.CacheException">
    /// Thrown when an error occurs while verifying the token's blacklisting status.
    /// </exception>
    Task<bool> IsTokenBlacklistedAsync(string token);
}