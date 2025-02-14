namespace TABP.Domain.Abstractions;

/// <summary>
/// Abstract class that serves as a base entity for all other entities in domain.
/// It provides a unique identifier of type<see cref="Guid"/>
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    /// <value>The unique identifier for this entity, represented as a<see cref="Guid"/>.</value>
    public Guid Id { get; set; }
}