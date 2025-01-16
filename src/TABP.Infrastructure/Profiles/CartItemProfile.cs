using AutoMapper;
using TABD.Domain.Models.CartItem;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Profiles;

internal class CartItemProfile : Profile
{
    public CartItemProfile()
    {
        CreateMap<CartItemDTO, CartItem>();
        CreateMap<CartItem, CartItemDTO>();
    }
}