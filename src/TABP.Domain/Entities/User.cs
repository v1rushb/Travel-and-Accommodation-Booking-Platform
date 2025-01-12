using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

public class User : Entity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public DateTime LastLogin { get; set; }

}