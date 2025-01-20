using AutoMapper;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.API.Profiles.Room;

internal class RoomProfiles : Profile
{
    public RoomProfiles()
    {
        CreateMap<RoomDTO, RoomForCreationDTO>();
        CreateMap<RoomForCreationDTO, RoomDTO>();
        CreateMap<RoomForAdminWithoutIdDTO, RoomDTO>();
    }
}