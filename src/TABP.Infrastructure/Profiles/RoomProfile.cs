using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.Infrastructure.Profiles;

internal class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<RoomDTO, Room>();
        CreateMap<Room, RoomDTO>();
        CreateMap<Room, RoomAdminResponseDTO>();
        CreateMap<RoomWithAvailability, RoomDTO>();
    }
}