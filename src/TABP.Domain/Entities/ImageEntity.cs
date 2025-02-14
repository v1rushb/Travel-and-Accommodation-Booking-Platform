using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents an image file and its path.
/// </summary>
public class ImageEntity : Entity
{
    /// <summary>
    /// The file path to the image stored in the system.
    /// </summary>
    public string Path { get; set; }
    
    /// <summary>
    /// The unique identifier of the Entity (e.g., Hotel, Room) this image is associated with.
    /// </summary>
    public Guid EntityId { get; set; }
}