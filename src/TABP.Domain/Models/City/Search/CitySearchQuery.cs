namespace TABP.Domain.Models.City.Search;

public class CitySearchQuery
{
    public string? SearchTerm { get; set; }
    public string? Country { get; set; }
    public Guid? Id { get; set; } = null;

    public override string ToString() =>
    @$"
            SearchTerm: {SearchTerm ?? "None"},
            Country: {Country ?? "None"}
            Id: {getIdStateString()}";

    private string getIdStateString() =>
        Id.HasValue ? Id.Value.ToString() : "None";

}