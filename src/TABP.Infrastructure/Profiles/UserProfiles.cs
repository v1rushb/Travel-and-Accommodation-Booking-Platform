using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.User;

namespace TABP.Infrastructure.Profiles;

internal class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<UserDTO, User>();
        CreateMap<User, UserDTO>();
    }
}