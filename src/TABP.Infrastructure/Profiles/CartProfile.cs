using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.Cart;

namespace TABP.Infrastructure.Profiles;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartDTO>();
        CreateMap<CartDTO, Cart>();
    }
}