using System.Text.Json;
using TABP.Domain.Models.Pagination;

namespace TABP.API.Extensions;

public static class ResponseHeaderExtensions
{
    private const string PaginationKeyName = "X-Pagination";
    public static void AddPaginationHeaders(
        this IHeaderDictionary headers,
        int totalItems,
        PaginationDTO pagination)
    {
        var paginationMetadata = new PaginationMetadataDTO(totalItems, pagination);
        var paginationMetadataJson = JsonSerializer.Serialize(paginationMetadata);
        headers.Append(PaginationKeyName, paginationMetadataJson);
    }
}