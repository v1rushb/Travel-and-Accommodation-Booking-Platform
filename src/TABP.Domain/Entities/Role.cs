using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents a user role within the application.
/// </summary>
public class Role : Entity
{
    /// <summary>
    /// The name of the role (e.g., "Admin", "User").
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// A description of what this role is for and what permissions it grants.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Navigation property to a collection of User entities that have this role.
    /// </summary>
    public ICollection<User> Users { get; set; } = new List<User>();
}