using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.Image;

public class ImageDTO
{
    public string Path { get; set; }

    public Guid EntityId { get; set; }
}