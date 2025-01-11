using AutoMapper;
using TABP.Domain.Models.User;

namespace TABP.API.Profiles.UserProfiles;

internal class UserRegisterationProfile : Profile
{
    public UserRegisterationProfile()
    {
        CreateMap<UserRegisterationProfile, UserDTO>();
    }
}