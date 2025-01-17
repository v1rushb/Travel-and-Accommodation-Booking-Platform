using AutoMapper;
using TABP.Domain.Models.Room;

namespace TABP.API.Profiles.Room;

internal class RoomProfiles : Profile
{
    public RoomProfiles()
    {
        CreateMap<RoomDTO, RoomForCreationDTO>();
        CreateMap<RoomForCreationDTO, RoomDTO>();
    }
}