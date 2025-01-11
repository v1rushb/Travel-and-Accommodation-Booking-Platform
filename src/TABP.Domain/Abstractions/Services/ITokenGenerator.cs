using TABP.Domain.Models.User;

namespace TABP.Domain.Abstractions.Services;

public interface ITokenGenerator
{
    string GenerateToken(UserDTO user);
}