using AutoMapper;
using TABP.Domain.Entities;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.Cart.Search.Response;

namespace TABP.Infrastructure.Profiles;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartDTO>();
        CreateMap<CartDTO, Cart>();

        CreateMap<Cart, CartAdminResponseDTO>();
        CreateMap<CartDTO, CartUserResponseDTO>();
        CreateMap<CartUserResponseDTO, CartDTO>();
        
    }
}