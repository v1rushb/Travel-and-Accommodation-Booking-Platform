using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Enums;
using TABP.Domain.Models.Pagination;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services.Cart;

namespace TABP.API.Controllers;

[Authorize(Roles = $"{nameof(RoleType.User)},{nameof(RoleType.Admin)}")]
[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(
        ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetCurrentCartItemsAsync(
        [FromQuery] PaginationDTO pagination)
    {
        var carts = await _cartService.GetCartItemsAsync(pagination);
        var cartsCount = carts.Count();

        Response.Headers.AddPaginationHeaders(cartsCount, pagination);
        return Ok(carts);
    }

    [HttpGet]
    public async Task<IActionResult> GetCartDetailsAsync()
    {
        var cart = await _cartService
            .GetCurrentCartDetailsAsync();

        return Ok(cart);
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> CheckOutCurrentCartAsync()
    {
        await _cartService.CheckOutAsync();

        return NoContent();
    }

    [HttpPost("new")]
    public async Task<IActionResult> CreateNewCartAsync()
    {
        await _cartService.CreateNewAsync();
        return NoContent();
    }
}