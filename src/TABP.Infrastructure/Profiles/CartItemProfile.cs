using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.CartItem;

namespace TABP.Infrastructure.Profiles;

internal class CartItemProfile : Profile
{
    public CartItemProfile()
    {
        CreateMap<CartItemDTO, CartItem>();
        CreateMap<CartItem, CartItemDTO>();
    }
}