using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

public class ImageEntity : Entity
{
    public string Path { get; set; }

    public Guid EntityId { get; set; }
}