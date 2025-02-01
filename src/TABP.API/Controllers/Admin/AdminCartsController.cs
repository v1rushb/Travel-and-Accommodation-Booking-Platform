using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services.Cart;
using TABP.Domain.Enums;
using TABP.Domain.Models.Cart.Search;
using TABP.Domain.Models.Pagination;

namespace TABP.API.Controllers.Admin;

[Authorize(Roles = nameof(RoleType.Admin))]
[ApiController]
[Route("api/admin/cart")]
public class AdminCartsController : ControllerBase
{
    private readonly ICartAdminService _cartAdminService;

    public AdminCartsController(
        ICartAdminService cartAdminService)
    {
        _cartAdminService = cartAdminService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchCartsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] CartSearchQuery query)
    {
        var carts = await _cartAdminService
            .SearchAsync(
                pagination,
                query
            );

        var cartCount = carts.Count();

        Response.Headers
            .AddPaginationHeaders(
                cartCount,
                pagination
            );

        return Ok(carts);
    }
}