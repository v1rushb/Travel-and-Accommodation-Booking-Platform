using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TABP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW vw_RoomWithAvailability AS
                SELECT
                    Room.Id,
                    Room.Number,
                    Room.Type,
                    Room.AdultsCapacity,
                    Room.ChildrenCapacity,
                    Room.PricePerNight,
                    Room.HotelId,
                    Hotel.Name AS HotelName,
                    Hotel.StarRating,
                    Room.CreationDate,
                    Room.ModificationDate,
                    CASE
                        WHEN EXISTS (
                            SELECT 1
                            FROM RoomBookings Booking
                            WHERE Booking.RoomId = Room.Id
                              AND GETDATE() BETWEEN Booking.CheckInDate AND Booking.CheckOutDate
                        ) THEN 0
                        ELSE 1
                    END AS IsAvailable
                FROM Rooms Room
                JOIN Hotels Hotel ON Room.HotelId = Hotel.Id;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vw_RoomWithAvailability");
        }
    }
}
