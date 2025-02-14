namespace TABP.Domain.Abstractions.Services;

/// <summary>
/// Provides a mechanism to commit changes to the database in a single operation.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves all pending changes asynchronously.
    /// </summary>
    Task SaveChangesAsync();
}