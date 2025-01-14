using TABP.Domain.Abstractions;
using TABP.Domain.Entities;
using TABP.Domain.Models.User;

namespace TABP.Domain.Models.Users
{
    public class UserWithRolesDTO : UserDTO
    {
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}