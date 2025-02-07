using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TABP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                CREATE VIEW vw_AvailableRooms AS
                SELECT
                    R.Id
                    R.Number,
                    R.Type,
                    R.AdultsCapacity,
                    R.ChildrenCapacity,
                    R.PricePerNight,
                    R.HotelId,
                    H.Name AS HotelName,
                    H.StarRating,
                    R.CreationDate,
                    R.ModificationDate
                FROM Rooms R
                JOIN Hotels H ON R.HotelId = H.Id
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM RoomBookings B
                    WHERE B.RoomId = R.Id
                    AND GETDATE() BETWEEN B.CheckInDate AND B.CheckOutDate
                );
                """);

            migrationBuilder.Sql("""
                CREATE VIEW vw_HotelRevenue AS
                SELECT 
                    H.Id AS HotelId,
                    H.Name,
                    H.BriefDescription,
                    H.DetailedDescription,
                    H.StarRating,
                    H.OwnerName,
                    H.Geolocation,
                    H.CreationDate,
                    H.ModificationDate,
                    H.CityId,
                    C.Name AS CityName,
                    C.CountryName AS CityCountryName,
                    SUM(R.PricePerNight * DATEDIFF(DAY, B.StartingDate, B.EndingDate)) AS TotalRevenue
                FROM Hotels H
                JOIN Rooms R ON H.Id = R.HotelId
                JOIN Bookings B ON R.Id = B.RoomId
                JOIN Cities C ON H.CityId = C.Id
                WHERE B.EndingDate < GETDATE()
                GROUP BY 
                    H.Id, 
                    H.Name, 
                    H.BriefDescription, 
                    H.DetailedDescription, 
                    H.StarRating, 
                    H.OwnerName, 
                    H.Geolocation, 
                    H.CreationDate, 
                    H.ModificationDate, 
                    H.CityId,
                    C.Name,
                    C.CountryName;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vw_AvailableRooms, vw_HotelRevenue");
        }
    }
}
