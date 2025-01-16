using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.Room;

namespace TABP.Infrastructure.Profiles;

internal class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<RoomDTO, Room>();
        CreateMap<Room, RoomDTO>();
    }
}