namespace TABP.Domain.Models.City.Search;

public class CitySearchQuery
{
    public string? SearchTerm { get; set; }
    public string? Country { get; set; }

    public override string ToString() =>
    @$"
            SearchTerm: {SearchTerm ?? "None"},
            Country: {Country ?? "None"}";
}