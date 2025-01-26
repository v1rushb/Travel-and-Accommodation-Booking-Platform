namespace TABP.Domain.Models.Configurations;

public class ConnectionStrings
{
    public required string SQLString { get; set; }
    public required string Redis { get; set; }
}