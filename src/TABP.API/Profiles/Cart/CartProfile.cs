using AutoMapper;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.CartItem;
using TABP.Domain.Models.RoomBooking;

namespace TABP.API.Profiles.Cart;

internal class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<RoomBookingForCreationDTO, CartItemDTO>();
    }
}