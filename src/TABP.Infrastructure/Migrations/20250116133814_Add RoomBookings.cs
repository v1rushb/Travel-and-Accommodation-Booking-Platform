using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TABP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomBooking_Rooms_RoomId",
                table: "RoomBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomBooking_Users_UserId",
                table: "RoomBooking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomBooking",
                table: "RoomBooking");

            migrationBuilder.RenameTable(
                name: "RoomBooking",
                newName: "RoomBookings");

            migrationBuilder.RenameColumn(
                name: "VisitDate",
                table: "HotelVisits",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "StartingDate",
                table: "RoomBookings",
                newName: "CheckOutDate");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "RoomBookings",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "EndingDate",
                table: "RoomBookings",
                newName: "CheckInDate");

            migrationBuilder.RenameIndex(
                name: "IX_RoomBooking_UserId",
                table: "RoomBookings",
                newName: "IX_RoomBookings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomBooking_RoomId",
                table: "RoomBookings",
                newName: "IX_RoomBookings_RoomId");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TimeSpent",
                table: "HotelVisits",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomBookings",
                table: "RoomBookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_Rooms_RoomId",
                table: "RoomBookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_Users_UserId",
                table: "RoomBookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookings_Rooms_RoomId",
                table: "RoomBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookings_Users_UserId",
                table: "RoomBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomBookings",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Discounts");

            migrationBuilder.RenameTable(
                name: "RoomBookings",
                newName: "RoomBooking");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "HotelVisits",
                newName: "VisitDate");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "RoomBooking",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "CheckOutDate",
                table: "RoomBooking",
                newName: "StartingDate");

            migrationBuilder.RenameColumn(
                name: "CheckInDate",
                table: "RoomBooking",
                newName: "EndingDate");

            migrationBuilder.RenameIndex(
                name: "IX_RoomBookings_UserId",
                table: "RoomBooking",
                newName: "IX_RoomBooking_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomBookings_RoomId",
                table: "RoomBooking",
                newName: "IX_RoomBooking_RoomId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeSpent",
                table: "HotelVisits",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomBooking",
                table: "RoomBooking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBooking_Rooms_RoomId",
                table: "RoomBooking",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBooking_Users_UserId",
                table: "RoomBooking",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
