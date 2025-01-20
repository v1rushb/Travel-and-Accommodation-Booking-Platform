namespace TABP.Domain.Models.Pagination;

public class PaginationDTO
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}