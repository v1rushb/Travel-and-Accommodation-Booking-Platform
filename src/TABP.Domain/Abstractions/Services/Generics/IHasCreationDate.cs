using TABP.Domain.Enums;

namespace TABP.Domain.Abstractions.Services.Generics;

/// <summary>
/// Represents a contract for any class that has a creation date.
/// mainly used for<see cref="TimeOptions"/>expression builders.
/// </summary>
public interface IHasCreationDate
{
    /// <summary>
    /// Gets or sets the creation date of the entity.
    /// </summary>
    /// <value>
    /// The date and time when the entity was created.
    /// </value>
    DateTime CreationDate { get; set; }
}