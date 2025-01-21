using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TABP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCartItemNewMetafields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddedAt",
                table: "CartItems",
                newName: "CreationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "CartItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "CartItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "CartItems",
                newName: "AddedAt");
        }
    }
}
