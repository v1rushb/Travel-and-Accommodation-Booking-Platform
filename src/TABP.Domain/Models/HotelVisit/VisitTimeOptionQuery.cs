namespace TABP.Domain.Models.HotelVisit;

public class VisitTimeOptionQuery
{
    public string? TimeOption { get; set; }

    public override string ToString() =>
        @$"
            TimeOption: {TimeOption ?? "None"}";
}