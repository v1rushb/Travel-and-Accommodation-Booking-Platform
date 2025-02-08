namespace TABP.Domain.Models.Room.Search;

public class RoomSearchQuery
{
    public int MinNumber { get; set; } = 0;
    public int MaxNumber { get; set; } = int.MaxValue;
    public IEnumerable<int> roomType { get; set; } 
    public int MinAdultsCapacity { get; set; } = 2;
    public int MaxAdultsCapacity { get; set; } = int.MaxValue;
    public int MinChildrenCapacity { get; set; } = 0;
    public int MaxChildrenCapacity { get; set; } = int.MaxValue;
    public int MinPricePerNight { get; set; } = 0;
    public int MaxPricePerNight { get; set; } = int.MaxValue;
    public Guid? Id { get; set; } = null;

    public override string ToString() =>
    @$"
                MinNumber: {MinNumber}{(MinNumber == 1 ? " (default)" : "")}, 
                MaxNumber: {MaxNumber}{(MaxNumber == int.MaxValue ? " (default)" : "")}, 
                RoomType: {(roomType != null && roomType.Any() ? string.Join(", ", roomType) : "None")}, 
                MinAdultsCapacity: {MinAdultsCapacity}{(MinAdultsCapacity == 2 ? " (default)" : "")}, 
                MaxAdultsCapacity: {MaxAdultsCapacity}{(MaxAdultsCapacity == int.MaxValue ? " (default)" : "")}, 
                MinChildrenCapacity: {MinChildrenCapacity}{(MinChildrenCapacity == 0 ? " (default)" : "")}, 
                MaxChildrenCapacity: {MaxChildrenCapacity}{(MaxChildrenCapacity == int.MaxValue ? " (default)" : "")}, 
                MinPricePerNight: {MinPricePerNight}{(MinPricePerNight == 0 ? " (default)" : "")}, 
                MaxPricePerNight: {MaxPricePerNight}{(MaxPricePerNight == int.MaxValue ? " (default)" : "")}
                Id: {GetIdStateString()}";

    private string GetIdStateString() =>
        Id.HasValue ? Id.Value.ToString() : "None";
}