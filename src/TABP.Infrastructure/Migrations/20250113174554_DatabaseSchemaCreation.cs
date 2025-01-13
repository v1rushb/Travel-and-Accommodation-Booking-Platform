using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TABP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseSchemaCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelVisit_Hotels_HotelId",
                table: "HotelVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelVisit_Users_UserId",
                table: "HotelVisit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelVisit",
                table: "HotelVisit");

            migrationBuilder.RenameTable(
                name: "HotelVisit",
                newName: "HotelVisits");

            migrationBuilder.RenameIndex(
                name: "IX_HotelVisit_UserId",
                table: "HotelVisits",
                newName: "IX_HotelVisits_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelVisit_HotelId",
                table: "HotelVisits",
                newName: "IX_HotelVisits_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelVisits",
                table: "HotelVisits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelVisits_Hotels_HotelId",
                table: "HotelVisits",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelVisits_Users_UserId",
                table: "HotelVisits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelVisits_Hotels_HotelId",
                table: "HotelVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelVisits_Users_UserId",
                table: "HotelVisits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelVisits",
                table: "HotelVisits");

            migrationBuilder.RenameTable(
                name: "HotelVisits",
                newName: "HotelVisit");

            migrationBuilder.RenameIndex(
                name: "IX_HotelVisits_UserId",
                table: "HotelVisit",
                newName: "IX_HotelVisit_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelVisits_HotelId",
                table: "HotelVisit",
                newName: "IX_HotelVisit_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelVisit",
                table: "HotelVisit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelVisit_Hotels_HotelId",
                table: "HotelVisit",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelVisit_Users_UserId",
                table: "HotelVisit",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
