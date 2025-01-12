using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.User;

public class UserDTO : Entity
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}