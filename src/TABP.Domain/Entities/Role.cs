using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

public class Role : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
}