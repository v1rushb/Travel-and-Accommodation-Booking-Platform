using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TABP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabaseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryName", "CreationDate", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("25eeca82-c189-4bbb-0209-08dd388dc7b9"), "Jordan", new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "Amman" },
                    { new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), "UK", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "London" },
                    { new Guid("7ca4b1aa-fa9c-40a2-5601-08dd46916b70"), "France", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Paris" },
                    { new Guid("e6554375-0932-462c-0207-08dd388dc7b9"), "Palestine", new DateTime(2025, 1, 19, 13, 32, 54, 831, DateTimeKind.Unspecified), new DateTime(2025, 1, 19, 13, 32, 54, 831, DateTimeKind.Unspecified), "Hebron" },
                    { new Guid("f3ebb6de-2c8f-42fc-0208-08dd388dc7b9"), "Palestine", new DateTime(2025, 1, 19, 13, 33, 39, 429, DateTimeKind.Unspecified), new DateTime(2025, 1, 19, 13, 33, 39, 429, DateTimeKind.Unspecified), "Safad" },
                    { new Guid("fe33e674-6c47-49dc-c1f7-08dd46922384"), "USA", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "New York" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("f5eea915-af74-46f2-e615-08dd5415a4a2"), "Administrator role", "Admin" },
                    { new Guid("f5eea915-af74-46f2-e615-08dd5415a4a4"), "Regular user role", "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastLogin", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("39ad172a-602e-4118-29d9-08dd398180c1"), "user4@gmail.com", "user4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user4", "AQAAAAIAAYagAAAAEPiNb8pIVNt5yr/azQOh/fuB3eDA15yC3FszFLpDQKowXsaFxCCfFQNkk/sUbrpYlw==", "user4" },
                    { new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5"), "user3@gmail.com", "user3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user3", "AQAAAAIAAYagAAAAEPiNb8pIVNt5yr/azQOh/fuB3eDA15yC3FszFLpDQKowXsaFxCCfFQNkk/sUbrpYlw==", "user3" },
                    { new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93"), "user5@gmail.com", "user5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user5", "AQAAAAIAAYagAAAAEPiNb8pIVNt5yr/azQOh/fuB3eDA15yC3FszFLpDQKowXsaFxCCfFQNkk/sUbrpYlw==", "user5" },
                    { new Guid("f5eea915-af74-46f2-e615-08dd5415a4d6"), "user2@gmail.com", "user2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2", "AQAAAAIAAYagAAAAEPiNb8pIVNt5yr/azQOh/fuB3eDA15yC3FszFLpDQKowXsaFxCCfFQNkk/sUbrpYlw==", "user2" },
                    { new Guid("f5eea915-af74-46f2-e615-08dd5415a4d7"), "admin@gmail.com", "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "AQAAAAIAAYagAAAAEPiNb8pIVNt5yr/azQOh/fuB3eDA15yC3FszFLpDQKowXsaFxCCfFQNkk/sUbrpYlw==", "admin" },
                    { new Guid("f5eea915-af74-46f2-e615-08dd5415a4d8"), "user@gmail.com", "user", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user", "AQAAAAIAAYagAAAAEPiNb8pIVNt5yr/azQOh/fuB3eDA15yC3FszFLpDQKowXsaFxCCfFQNkk/sUbrpYlw==", "user" },
                    { new Guid("f5eea915-af74-46f2-e615-08dd5415a4d9"), "cs.bashar.herbawi@gmail.com", "Bashar", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Herbawi", "AQAAAAIAAYagAAAAEPiNb8pIVNt5yr/azQOh/fuB3eDA15yC3FszFLpDQKowXsaFxCCfFQNkk/sUbrpYlw==", "v1rushb" },
                    { new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75"), "user6@gmail.com", "user6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user6", "AQAAAAIAAYagAAAAEPiNb8pIVNt5yr/azQOh/fuB3eDA15yC3FszFLpDQKowXsaFxCCfFQNkk/sUbrpYlw==", "user6" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "BriefDescription", "CityId", "CreationDate", "DetailedDescription", "Geolocation", "ModificationDate", "Name", "OwnerName", "StarRating" },
                values: new object[,]
                {
                    { new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), "A 4-star hotel in the heart of Times Square.", new Guid("fe33e674-6c47-49dc-c1f7-08dd46922384"), new DateTime(2024, 2, 9, 19, 3, 10, 588, DateTimeKind.Unspecified).AddTicks(5700), "Stay in the heart of Times Square...", "40.7566480079687,-73.98881546193508", new DateTime(2024, 2, 9, 19, 3, 10, 588, DateTimeKind.Unspecified).AddTicks(5704), "Hilton New York Times Square", "Hilton", 0m },
                    { new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), "A 4-star hotel located above Tower Hill Underground Station.", new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new DateTime(2024, 2, 9, 18, 54, 53, 685, DateTimeKind.Unspecified).AddTicks(569), "Experience luxury and convenience...", "51.510223410295524,-0.07644353237381915", new DateTime(2024, 2, 9, 20, 45, 21, 12, DateTimeKind.Unspecified).AddTicks(2021), "citizenM Tower Of London hotel", "citizenM", 0m },
                    { new Guid("75ae0504-974c-4ff2-ab13-c30374ac8558"), "A 4-star hotel", new Guid("e6554375-0932-462c-0207-08dd388dc7b9"), new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "Stay in the heart of Hebron", "42.7566480079687,-74.98881546193508", new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "Abu Mazen", "Abu Mazen maybe", 0m },
                    { new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), "A 4-star hotel offering panoramic views of Paris.", new Guid("7ca4b1aa-fa9c-40a2-5601-08dd46916b70"), new DateTime(2024, 2, 9, 18, 56, 58, 173, DateTimeKind.Unspecified).AddTicks(3565), "The 4-star Pullman Paris Tour Eiffel hotel...", "48.85567419020331,2.2928680490125637", new DateTime(2024, 2, 9, 18, 56, 58, 173, DateTimeKind.Unspecified).AddTicks(3570), "Pullman Paris Tour Eiffel", "Pullman", 0m },
                    { new Guid("ceb2b836-3b8c-4ea9-93cc-f5c442d5d966"), "A 4-star hotel in Hebron", new Guid("e6554375-0932-462c-0207-08dd388dc7b9"), new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "Stay in the heart of Hebron", "20.7566480079687,-7.98881546193508", new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "Burj Herbawi 2", "Bashar", 0m },
                    { new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), "A 4-star hotel in Amman", new Guid("25eeca82-c189-4bbb-0209-08dd388dc7b9"), new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "Stay in the heart of Amman", "40.7566480079687,-73.98881546193508", new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "Burj Herbawi", "Bashar", 0m }
                });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("f5eea915-af74-46f2-e615-08dd5415a4a2"), new Guid("f5eea915-af74-46f2-e615-08dd5415a4d7") },
                    { new Guid("f5eea915-af74-46f2-e615-08dd5415a4a2"), new Guid("f5eea915-af74-46f2-e615-08dd5415a4d9") },
                    { new Guid("f5eea915-af74-46f2-e615-08dd5415a4a4"), new Guid("f5eea915-af74-46f2-e615-08dd5415a4d6") },
                    { new Guid("f5eea915-af74-46f2-e615-08dd5415a4a4"), new Guid("f5eea915-af74-46f2-e615-08dd5415a4d8") }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "AmountPercentage", "CreationDate", "EndingDate", "HotelId", "ModificationDate", "Reason", "StartingDate", "roomType" },
                values: new object[,]
                {
                    { new Guid("027e91be-e337-4b48-ac33-a6ee6992d708"), 30m, new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new DateTime(2025, 4, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), new DateTime(2025, 2, 9, 19, 6, 19, 661, DateTimeKind.Unspecified), "New season", new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), 1 },
                    { new Guid("3329cf25-8cc8-4034-b032-6e1db78e4dd9"), 17m, new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new DateTime(2025, 4, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), new DateTime(2025, 3, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "New season", new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), 1 },
                    { new Guid("335126f5-ea9a-49a0-978a-9503d04449db"), 15m, new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new DateTime(2025, 4, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "New season", new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), 1 },
                    { new Guid("892944e1-6c20-4530-a865-250989e23248"), 15m, new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new DateTime(2025, 4, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new Guid("75ae0504-974c-4ff2-ab13-c30374ac8558"), new DateTime(2025, 4, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "New season", new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), 1 },
                    { new Guid("cda35c4b-d597-4186-afe7-26bd3af94397"), 30m, new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new DateTime(2025, 4, 9, 19, 6, 19, 661, DateTimeKind.Unspecified), new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "New season", new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), 2 },
                    { new Guid("d7a473e6-ec2f-48d6-852a-2e68c9993f9b"), 30m, new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new DateTime(2025, 4, 9, 19, 6, 19, 661, DateTimeKind.Unspecified), new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "Why not", new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), 0 },
                    { new Guid("f0a22dda-4769-4509-913a-1be8a8d5b88f"), 10m, new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new DateTime(2025, 4, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new DateTime(2025, 4, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), "New season", new DateTime(2025, 1, 19, 13, 34, 10, 187, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "HotelReviews",
                columns: new[] { "Id", "CreationDate", "Feedback", "HotelId", "ModificationDate", "Rating", "UserId" },
                values: new object[,]
                {
                    { new Guid("0d1e2f3a-b4c5-4d2c-6d7e-8f9a0b1c2d3e"), new DateTime(2025, 9, 2, 18, 20, 0, 0, DateTimeKind.Unspecified), "Location is not ideal for tourists, but okay for business.", new Guid("75ae0504-974c-4ff2-ab13-c30374ac8558"), new DateTime(2025, 9, 2, 18, 20, 0, 0, DateTimeKind.Unspecified), 3, new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("1a2b3c4d-e5f6-4def-7a8b-c9d0e1f2a3b4"), new DateTime(2025, 6, 18, 11, 30, 0, 0, DateTimeKind.Unspecified), "Service was top-notch, highly recommend.", new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2025, 6, 18, 11, 30, 0, 0, DateTimeKind.Unspecified), 5, new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("1e2f3a4b-c5d6-4d3d-7e8f-9a0b1c2d3e4f"), new DateTime(2025, 7, 1, 11, 15, 0, 0, DateTimeKind.Unspecified), "Great location in Amman, close to everything.", new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), new DateTime(2025, 7, 1, 11, 15, 0, 0, DateTimeKind.Unspecified), 4, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("2b3c4d5e-f6a7-4fae-8b9c-0d1e2f3a4b5c"), new DateTime(2025, 7, 5, 17, 15, 0, 0, DateTimeKind.Unspecified), "A bit pricey but worth it for the view.", new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2025, 7, 5, 17, 15, 0, 0, DateTimeKind.Unspecified), 3, new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("2f3a4b5c-d6e7-4d4e-8f9a-0b1c2d3e4f5a"), new DateTime(2025, 8, 10, 15, 55, 0, 0, DateTimeKind.Unspecified), "Helpful staff and comfortable rooms.", new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), new DateTime(2025, 8, 10, 15, 55, 0, 0, DateTimeKind.Unspecified), 3, new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("3a4b5c6d-e7f8-4d5f-9a0b-1c2d3e4f5a6b"), new DateTime(2025, 9, 25, 9, 5, 0, 0, DateTimeKind.Unspecified), "Breakfast was okay, but overall a pleasant stay.", new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), new DateTime(2025, 9, 25, 9, 5, 0, 0, DateTimeKind.Unspecified), 3, new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("3c4d5e6f-a7b8-4fbf-9c0d-1e2f3a4b5c6d"), new DateTime(2025, 5, 1, 8, 50, 0, 0, DateTimeKind.Unspecified), "Perfect location for exploring Times Square.", new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), new DateTime(2025, 5, 1, 8, 50, 0, 0, DateTimeKind.Unspecified), 4, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("4b5c6d7e-f8a9-4d6a-0b1c-2d3e4f5a6b7c"), new DateTime(2025, 10, 5, 14, 30, 0, 0, DateTimeKind.Unspecified), "Could be cleaner, but decent for the price.", new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), new DateTime(2025, 10, 5, 14, 30, 0, 0, DateTimeKind.Unspecified), 3, new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("4d5e6f7a-b8c9-4cc0-0d1e-2f3a4b5c6d7e"), new DateTime(2025, 6, 12, 13, 25, 0, 0, DateTimeKind.Unspecified), "Comfortable and clean rooms, great amenities.", new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), new DateTime(2025, 6, 12, 13, 25, 0, 0, DateTimeKind.Unspecified), 3, new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("5c6d7e8f-9a0b-4d7b-1c2d-3e4f5a6b7c8d"), new DateTime(2025, 8, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Quiet location, good for relaxing.", new Guid("ceb2b836-3b8c-4ea9-93cc-f5c442d5d966"), new DateTime(2025, 8, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("5e6f7a8b-c9d0-4cd1-1e2f-3a4b5c6d7e8f"), new DateTime(2025, 7, 20, 10, 10, 0, 0, DateTimeKind.Unspecified), "Good for a short stay, a bit busy though.", new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), new DateTime(2025, 7, 20, 10, 10, 0, 0, DateTimeKind.Unspecified), 3, new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("6d7e8f9a-0b1c-4d8c-2d3e-4f5a6b7c8d9e"), new DateTime(2025, 9, 15, 16, 40, 0, 0, DateTimeKind.Unspecified), "Basic amenities, suitable for a short stay.", new Guid("ceb2b836-3b8c-4ea9-93cc-f5c442d5d966"), new DateTime(2025, 9, 15, 16, 40, 0, 0, DateTimeKind.Unspecified), 3, new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("6f7a8b9c-d0e1-4ce2-2f3a-4b5c6d7e8f9a"), new DateTime(2025, 8, 1, 19, 55, 0, 0, DateTimeKind.Unspecified), "Location is amazing but hotel feels dated.", new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), new DateTime(2025, 8, 1, 19, 55, 0, 0, DateTimeKind.Unspecified), 4, new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("7a8b9c0d-e1f2-4cf3-3a4b-5c6d7e8f9a0b"), new DateTime(2025, 6, 1, 15, 40, 0, 0, DateTimeKind.Unspecified), "Authentic experience, great hospitality.", new Guid("75ae0504-974c-4ff2-ab13-c30374ac8558"), new DateTime(2025, 6, 1, 15, 40, 0, 0, DateTimeKind.Unspecified), 4, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("7e8f9a0b-1c2d-4d9d-3e4f-5a6b7c8d9e0f"), new DateTime(2025, 10, 22, 11, 20, 0, 0, DateTimeKind.Unspecified), "Friendly staff, but facilities are limited.", new Guid("ceb2b836-3b8c-4ea9-93cc-f5c442d5d966"), new DateTime(2025, 10, 22, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("8b9c0d1e-f2a3-4d0a-4b5c-6d7e8f9a0b1c"), new DateTime(2025, 7, 12, 21, 0, 0, 0, DateTimeKind.Unspecified), "Simple but clean, friendly staff.", new Guid("75ae0504-974c-4ff2-ab13-c30374ac8558"), new DateTime(2025, 7, 12, 21, 0, 0, 0, DateTimeKind.Unspecified), 3, new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("8f9a0b1c-2d3e-4dae-4f5a-6b7c8d9e0f1a"), new DateTime(2025, 11, 1, 20, 0, 0, 0, DateTimeKind.Unspecified), "Not the best, needs improvement.", new Guid("ceb2b836-3b8c-4ea9-93cc-f5c442d5d966"), new DateTime(2025, 11, 1, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("9c0d1e2f-a3b4-4d1b-5c6d-7e8f9a0b1c2d"), new DateTime(2025, 8, 19, 12, 50, 0, 0, DateTimeKind.Unspecified), "Value for money, good for budget travelers.", new Guid("75ae0504-974c-4ff2-ab13-c30374ac8558"), new DateTime(2025, 8, 19, 12, 50, 0, 0, DateTimeKind.Unspecified), 3, new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("a1b2c3d4-e5f6-4789-1a2b-c3d4e5f6a7b8"), new DateTime(2025, 3, 10, 14, 20, 0, 0, DateTimeKind.Unspecified), "Great location, modern and clean rooms.", new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new DateTime(2025, 3, 10, 14, 20, 0, 0, DateTimeKind.Unspecified), 5, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("b2c3d4e5-f6a7-489a-2b3c-d4e5f6a7b8c9"), new DateTime(2025, 4, 15, 18, 30, 0, 0, DateTimeKind.Unspecified), "Sleek hotel with friendly staff and amazing views.", new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new DateTime(2025, 4, 15, 18, 30, 0, 0, DateTimeKind.Unspecified), 4, new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("c3d4e5f6-a7b8-49ab-3c4d-e5f6a7b8c9d0"), new DateTime(2025, 5, 22, 9, 45, 0, 0, DateTimeKind.Unspecified), "Excellent stay, breakfast was delicious.", new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new DateTime(2025, 5, 22, 9, 45, 0, 0, DateTimeKind.Unspecified), 5, new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("d4e5f6a7-b8c9-4bcd-4d5e-f6a7b8c9d0e1"), new DateTime(2025, 6, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), "A bit noisy but overall a good experience.", new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new DateTime(2025, 6, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), 3, new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("e5f6a7b8-c9d0-4cde-5e6f-a7b8c9d0e1f2"), new DateTime(2025, 4, 1, 12, 10, 0, 0, DateTimeKind.Unspecified), "Unbeatable views of the Eiffel Tower, luxurious.", new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2025, 4, 1, 12, 10, 0, 0, DateTimeKind.Unspecified), 5, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("f6a7b8c9-d0e1-4def-6f7a-b8c9d0e1f2a3"), new DateTime(2025, 5, 10, 20, 40, 0, 0, DateTimeKind.Unspecified), "Wonderful location, rooms are very comfortable.", new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2025, 5, 10, 20, 40, 0, 0, DateTimeKind.Unspecified), 4, new Guid("39ad172a-602e-4118-29d9-08dd398180c1") }
                });

            migrationBuilder.InsertData(
                table: "HotelVisits",
                columns: new[] { "Id", "CreationDate", "HotelId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0e1f2a3b-4c5d-466e-7f8a-9b0c1d2e3f4a"), new DateTime(2025, 4, 12, 14, 20, 0, 0, DateTimeKind.Unspecified), new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("11111111-2222-3333-4444-555555555555"), new DateTime(2026, 2, 28, 16, 30, 0, 0, DateTimeKind.Unspecified), new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("1b2c3d4e-f5a6-487b-8c9d-0e1f2a3b4c5d"), new DateTime(2025, 5, 10, 19, 50, 0, 0, DateTimeKind.Unspecified), new Guid("ceb2b836-3b8c-4ea9-93cc-f5c442d5d966"), new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("1f2a3b4c-5d6e-477f-8a9b-0c1d2e3f4a5b"), new DateTime(2025, 6, 5, 18, 10, 0, 0, DateTimeKind.Unspecified), new Guid("75ae0504-974c-4ff2-ab13-c30374ac8558"), new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("2a3b4c5d-6e7f-488a-9b0c-1d2e3f4a5b6c"), new DateTime(2025, 8, 1, 22, 50, 0, 0, DateTimeKind.Unspecified), new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("2c3d4e5f-6a7b-498c-9d0e-1f2a3b4c5d6e"), new DateTime(2024, 12, 19, 12, 30, 0, 0, DateTimeKind.Unspecified), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("3b4c5d6e-7f8a-499b-0c1d-2e3f4a5b6c7d"), new DateTime(2025, 10, 25, 10, 30, 0, 0, DateTimeKind.Unspecified), new Guid("ceb2b836-3b8c-4ea9-93cc-f5c442d5d966"), new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("3d4e5f6a-7b8c-4a9d-0e1f-2a3b4c5d6e7f"), new DateTime(2025, 3, 1, 15, 40, 0, 0, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("4c5d6e7f-8a9b-4a0c-1d2e-3f4a5b6c7d8e"), new DateTime(2025, 2, 20, 13, 55, 0, 0, DateTimeKind.Unspecified), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("4e5f6a7b-8c9d-4b0e-1f2a-3b4c5d6e7f8a"), new DateTime(2025, 5, 2, 17, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("5d6e7f8a-9b0c-4b1d-2e3f-4a5b6c7d8e9f"), new DateTime(2025, 4, 5, 16, 10, 0, 0, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("5f6a7b8c-9d0e-4c1f-2a3b-4c5d6e7f8a9b"), new DateTime(2025, 7, 10, 8, 55, 0, 0, DateTimeKind.Unspecified), new Guid("75ae0504-974c-4ff2-ab13-c30374ac8558"), new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("6a7b8c9d-0e1f-4d2a-3b4c-5d6e7f8a9b0c"), new DateTime(2025, 9, 3, 13, 25, 0, 0, DateTimeKind.Unspecified), new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("6e7f8a9b-0c1d-4c2e-3f4a-5b6c7d8e9f0a"), new DateTime(2025, 6, 12, 9, 45, 0, 0, DateTimeKind.Unspecified), new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("7b8c9d0e-1f2a-4e3b-4c5d-6e7f8a9b0c1d"), new DateTime(2025, 11, 18, 20, 10, 0, 0, DateTimeKind.Unspecified), new Guid("ceb2b836-3b8c-4ea9-93cc-f5c442d5d966"), new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("7f8a9b0c-1d2e-4d3f-4a5b-6c7d8e9f0a1b"), new DateTime(2025, 8, 18, 15, 30, 0, 0, DateTimeKind.Unspecified), new Guid("75ae0504-974c-4ff2-ab13-c30374ac8558"), new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("8a9b0c1d-2e3f-4e4a-5b6c-7d8e9f0a1b2c"), new DateTime(2025, 10, 1, 21, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("8c9d0e1f-2a3b-4f4c-5d6e-7f8a9b0c1d2e"), new DateTime(2025, 1, 18, 11, 40, 0, 0, DateTimeKind.Unspecified), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("9d0e1f2a-3b4c-455d-6e7f-8a9b0c1d2e3f"), new DateTime(2025, 2, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("a1a1a1a1-b2b2-c3c3-d4d4-e5e5e5e5e5e5"), new DateTime(2026, 1, 15, 17, 20, 0, 0, DateTimeKind.Unspecified), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("b1a2c3d4-e5f6-4789-9a0b-c1d2e3f4a5b6"), new DateTime(2024, 12, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("b1b1b1b1-c2c2-d3d3-e4e4-f5f5f5f5f5f5"), new DateTime(2026, 2, 10, 8, 30, 0, 0, DateTimeKind.Unspecified), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("c1c1c1c1-d2d2-e3e3-f4f4-a5a5a5a5a5a5"), new DateTime(2026, 3, 1, 19, 40, 0, 0, DateTimeKind.Unspecified), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("c7d8e9f0-1a2b-4c5d-3e4f-5a6b7c8d9e0f"), new DateTime(2025, 1, 10, 14, 30, 0, 0, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("d1d1d1d1-e2e2-f3f3-a4a4-b5b5b5b5b5b5"), new DateTime(2026, 1, 20, 14, 50, 0, 0, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("d3e4f5a6-b7c8-4d9e-5f0a-1b2c3d4e5f6a"), new DateTime(2025, 2, 1, 9, 15, 0, 0, DateTimeKind.Unspecified), new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("e1e1e1e1-f2f2-a3a3-b4b4-c5c5c5c5c5c5"), new DateTime(2026, 2, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("e9f0a1b2-c3d4-4e5f-6a7b-8c9d0e1f2a3b"), new DateTime(2025, 3, 15, 16, 45, 0, 0, DateTimeKind.Unspecified), new Guid("75ae0504-974c-4ff2-ab13-c30374ac8558"), new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("f1f1f1f1-a2a2-b3b3-c4c4-d5d5d5d5d5d5"), new DateTime(2026, 1, 25, 9, 10, 0, 0, DateTimeKind.Unspecified), new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"), new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("f5a6b7c8-d9e0-4f1a-7b8c-9d0e1f2a3b4c"), new DateTime(2025, 4, 20, 11, 20, 0, 0, DateTimeKind.Unspecified), new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"), new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AdultsCapacity", "ChildrenCapacity", "CreationDate", "HotelId", "ModificationDate", "Number", "PricePerNight", "Type" },
                values: new object[,]
                {
                    { new Guid("28e17d69-9e0e-44a3-2947-08dc29a4de37"), 2, 3, new DateTime(2024, 3, 9, 18, 54, 53, 685, DateTimeKind.Unspecified).AddTicks(569), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new DateTime(2024, 3, 9, 19, 25, 25, 924, DateTimeKind.Unspecified).AddTicks(5743), 1, 50, 0 },
                    { new Guid("3914316b-1b87-46f7-294f-08dc29a4de37"), 2, 2, new DateTime(2025, 1, 19, 14, 34, 10, 187, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2025, 1, 19, 14, 34, 10, 187, DateTimeKind.Unspecified), 3, 75, 0 },
                    { new Guid("601a0d84-0435-4221-294e-08dc29a4de37"), 2, 0, new DateTime(2025, 1, 19, 14, 34, 10, 187, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2025, 1, 19, 14, 34, 10, 187, DateTimeKind.Unspecified), 2, 100, 0 },
                    { new Guid("a914be7e-c545-4784-2948-08dc29a4de37"), 2, 0, new DateTime(2024, 2, 9, 19, 25, 46, 76, DateTimeKind.Unspecified).AddTicks(2941), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new DateTime(2024, 2, 9, 19, 25, 46, 76, DateTimeKind.Unspecified).AddTicks(2945), 2, 100, 1 },
                    { new Guid("b3f07752-2d97-4b2c-2949-08dc29a4de37"), 3, 2, new DateTime(2024, 2, 9, 19, 26, 3, 594, DateTimeKind.Unspecified).AddTicks(2376), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new DateTime(2024, 2, 9, 19, 26, 3, 594, DateTimeKind.Unspecified).AddTicks(2379), 3, 75, 0 },
                    { new Guid("b3f52184-3330-4750-294c-08dc29a4de37"), 2, 3, new DateTime(2024, 2, 9, 19, 26, 58, 930, DateTimeKind.Unspecified).AddTicks(8602), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 26, 58, 930, DateTimeKind.Unspecified).AddTicks(8604), 1, 50, 0 },
                    { new Guid("c9a9f0d0-1111-4444-8888-1234567890ab"), 4, 1, new DateTime(2025, 1, 9, 20, 0, 0, 0, DateTimeKind.Unspecified), new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"), new DateTime(2025, 1, 9, 20, 0, 0, 0, DateTimeKind.Unspecified), 4, 200, 1 },
                    { new Guid("d1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890"), 2, 1, new DateTime(2025, 1, 19, 14, 34, 10, 187, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2025, 1, 19, 14, 34, 10, 187, DateTimeKind.Unspecified), 3, 180, 1 },
                    { new Guid("e2f3a4b5-c6d7-8901-e2f3-a4b5c6d78901"), 3, 1, new DateTime(2025, 1, 19, 14, 34, 10, 187, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2025, 1, 19, 14, 34, 10, 187, DateTimeKind.Unspecified), 4, 220, 1 },
                    { new Guid("e8a1a3e6-d8da-4928-294b-08dc29a4de37"), 2, 0, new DateTime(2024, 2, 9, 19, 26, 39, 972, DateTimeKind.Unspecified).AddTicks(3640), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 26, 39, 972, DateTimeKind.Unspecified).AddTicks(3645), 2, 100, 1 },
                    { new Guid("f15bd95c-8746-4236-294a-08dc29a4de37"), 3, 2, new DateTime(2024, 2, 9, 19, 26, 22, 383, DateTimeKind.Unspecified).AddTicks(9118), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 26, 22, 383, DateTimeKind.Unspecified).AddTicks(9124), 1, 75, 0 },
                    { new Guid("f339b369-2a05-4eea-294d-08dc29a4de37"), 2, 3, new DateTime(2025, 1, 19, 14, 34, 10, 187, DateTimeKind.Unspecified), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2025, 1, 19, 14, 34, 10, 187, DateTimeKind.Unspecified), 1, 50, 0 }
                });

            migrationBuilder.InsertData(
                table: "RoomBookings",
                columns: new[] { "Id", "CheckInDate", "CheckOutDate", "CreationDate", "ModificationDate", "Notes", "RoomId", "Status", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-e5f6-4c92-3031-323334353637"), new DateTime(2025, 7, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 15, 19, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 15, 19, 15, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("3914316b-1b87-46f7-294f-08dc29a4de37"), 0, 0m, new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("2b3c4d5e-f6a7-4d0a-3334-353637383940"), new DateTime(2025, 8, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("c9a9f0d0-1111-4444-8888-1234567890ab"), 0, 0m, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("3c4d5e6f-a7b8-4e1b-3637-383940414243"), new DateTime(2025, 9, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 4, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 10, 21, 40, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 10, 21, 40, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("d1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890"), 0, 0m, new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("4d5e6f7a-b8c9-4f2c-3940-414243444546"), new DateTime(2025, 10, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 30, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 1, 7, 20, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 1, 7, 20, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("e2f3a4b5-c6d7-8901-e2f3-a4b5c6d78901"), 0, 0m, new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("5e6f7a8b-c9d0-4d3d-4243-444546474849"), new DateTime(2025, 11, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 7, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 15, 15, 50, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 15, 15, 50, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("28e17d69-9e0e-44a3-2947-08dc29a4de37"), 0, 0m, new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("61237e1a-928b-494a-be63-d9b562a65896"), new DateTime(2025, 3, 6, 12, 12, 54, 295, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 13, 8, 36, 295, DateTimeKind.Unspecified), new DateTime(2025, 2, 7, 11, 0, 19, 389, DateTimeKind.Unspecified), new DateTime(2025, 2, 7, 11, 0, 19, 389, DateTimeKind.Unspecified), "Yoink", new Guid("e2f3a4b5-c6d7-8901-e2f3-a4b5c6d78901"), 0, 0m, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("6401b407-35d4-4fe6-a1ba-ecf945440fbf"), new DateTime(2025, 1, 6, 12, 12, 54, 295, DateTimeKind.Unspecified), new DateTime(2025, 1, 9, 13, 8, 36, 295, DateTimeKind.Unspecified), new DateTime(2025, 2, 7, 11, 0, 19, 389, DateTimeKind.Unspecified), new DateTime(2025, 2, 7, 11, 0, 19, 389, DateTimeKind.Unspecified), "Yoink", new Guid("c9a9f0d0-1111-4444-8888-1234567890ab"), 0, 0m, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("6f7a8b9c-d1e2-4e4e-4546-474849505152"), new DateTime(2025, 12, 22, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 1, 18, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 1, 18, 30, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("a914be7e-c545-4784-2948-08dc29a4de37"), 0, 0m, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("7a8b9c0d-e2f3-4f5f-4849-505152535455"), new DateTime(2026, 1, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 10, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 10, 22, 0, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("f15bd95c-8746-4236-294a-08dc29a4de37"), 0, 0m, new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("8b9c0d1e-f3a4-416a-5152-535455565758"), new DateTime(2026, 2, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 5, 11, 11, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 5, 11, 11, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("e8a1a3e6-d8da-4928-294b-08dc29a4de37"), 0, 0m, new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("97d8a55f-ac24-4e02-87f7-48b2a101c0c1"), new DateTime(2025, 4, 6, 12, 12, 54, 295, DateTimeKind.Unspecified), new DateTime(2025, 5, 9, 13, 8, 36, 295, DateTimeKind.Unspecified), new DateTime(2025, 2, 7, 11, 0, 19, 389, DateTimeKind.Unspecified), new DateTime(2025, 2, 7, 11, 0, 19, 389, DateTimeKind.Unspecified), "Yoink", new Guid("d1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890"), 0, 0m, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("9c0d1e2f-a4b5-4b7b-5455-565758596061"), new DateTime(2026, 3, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 20, 17, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 20, 17, 45, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("b3f52184-3330-4750-294c-08dc29a4de37"), 0, 0m, new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("a1b2c3d4-e5f6-4789-1011-121314151617"), new DateTime(2024, 12, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 24, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 15, 9, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 15, 9, 30, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("28e17d69-9e0e-44a3-2947-08dc29a4de37"), 0, 0m, new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("b7c8d9e0-f1a2-4b5c-1314-151617181920"), new DateTime(2025, 1, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 22, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 1, 14, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 1, 14, 45, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("a914be7e-c545-4784-2948-08dc29a4de37"), 0, 0m, new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") },
                    { new Guid("bdb25542-aad6-41fc-9c5d-61debda238e0"), new DateTime(2025, 1, 6, 12, 12, 54, 295, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 13, 8, 36, 295, DateTimeKind.Unspecified), new DateTime(2025, 2, 7, 11, 0, 19, 389, DateTimeKind.Unspecified), new DateTime(2025, 2, 7, 11, 0, 19, 389, DateTimeKind.Unspecified), "Yoink", new Guid("c9a9f0d0-1111-4444-8888-1234567890ab"), 0, 0m, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("c3d4e5f6-a7b8-4d9e-1617-181920212223"), new DateTime(2025, 2, 28, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 10, 10, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 10, 10, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("f15bd95c-8746-4236-294a-08dc29a4de37"), 0, 0m, new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75") },
                    { new Guid("d5e6f7a8-b9c0-4e1f-2021-222324252627"), new DateTime(2025, 4, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 16, 20, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 16, 20, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("e8a1a3e6-d8da-4928-294b-08dc29a4de37"), 0, 0m, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("e1f2a3b4-c5d6-4f7a-2324-252627282930"), new DateTime(2025, 5, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 1, 11, 55, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 1, 11, 55, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("b3f52184-3330-4750-294c-08dc29a4de37"), 0, 0m, new Guid("39ad172a-602e-4118-29d9-08dd398180c1") },
                    { new Guid("f7422ce9-5e89-4b49-08ca-08dd47669c80"), new DateTime(2025, 2, 10, 12, 12, 54, 295, DateTimeKind.Unspecified), new DateTime(2025, 2, 19, 13, 8, 36, 295, DateTimeKind.Unspecified), new DateTime(2025, 2, 7, 11, 0, 19, 389, DateTimeKind.Unspecified), new DateTime(2025, 2, 7, 11, 0, 19, 389, DateTimeKind.Unspecified), "Yoink", new Guid("d1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890"), 0, 0m, new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5") },
                    { new Guid("f9a0b1c2-d3e4-418b-2627-282930313233"), new DateTime(2025, 6, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), "Yoink", new Guid("601a0d84-0435-4221-294e-08dc29a4de37"), 0, 0m, new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f3ebb6de-2c8f-42fc-0208-08dd388dc7b9"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("027e91be-e337-4b48-ac33-a6ee6992d708"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("3329cf25-8cc8-4034-b032-6e1db78e4dd9"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("335126f5-ea9a-49a0-978a-9503d04449db"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("892944e1-6c20-4530-a865-250989e23248"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("cda35c4b-d597-4186-afe7-26bd3af94397"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("d7a473e6-ec2f-48d6-852a-2e68c9993f9b"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("f0a22dda-4769-4509-913a-1be8a8d5b88f"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("0d1e2f3a-b4c5-4d2c-6d7e-8f9a0b1c2d3e"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("1a2b3c4d-e5f6-4def-7a8b-c9d0e1f2a3b4"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("1e2f3a4b-c5d6-4d3d-7e8f-9a0b1c2d3e4f"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("2b3c4d5e-f6a7-4fae-8b9c-0d1e2f3a4b5c"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("2f3a4b5c-d6e7-4d4e-8f9a-0b1c2d3e4f5a"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("3a4b5c6d-e7f8-4d5f-9a0b-1c2d3e4f5a6b"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("3c4d5e6f-a7b8-4fbf-9c0d-1e2f3a4b5c6d"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("4b5c6d7e-f8a9-4d6a-0b1c-2d3e4f5a6b7c"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("4d5e6f7a-b8c9-4cc0-0d1e-2f3a4b5c6d7e"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("5c6d7e8f-9a0b-4d7b-1c2d-3e4f5a6b7c8d"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("5e6f7a8b-c9d0-4cd1-1e2f-3a4b5c6d7e8f"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("6d7e8f9a-0b1c-4d8c-2d3e-4f5a6b7c8d9e"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("6f7a8b9c-d0e1-4ce2-2f3a-4b5c6d7e8f9a"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("7a8b9c0d-e1f2-4cf3-3a4b-5c6d7e8f9a0b"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("7e8f9a0b-1c2d-4d9d-3e4f-5a6b7c8d9e0f"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("8b9c0d1e-f2a3-4d0a-4b5c-6d7e8f9a0b1c"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("8f9a0b1c-2d3e-4dae-4f5a-6b7c8d9e0f1a"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("9c0d1e2f-a3b4-4d1b-5c6d-7e8f9a0b1c2d"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-1a2b-c3d4e5f6a7b8"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-489a-2b3c-d4e5f6a7b8c9"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-49ab-3c4d-e5f6a7b8c9d0"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-4bcd-4d5e-f6a7b8c9d0e1"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-4cde-5e6f-a7b8c9d0e1f2"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-d0e1-4def-6f7a-b8c9d0e1f2a3"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("0e1f2a3b-4c5d-466e-7f8a-9b0c1d2e3f4a"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("1b2c3d4e-f5a6-487b-8c9d-0e1f2a3b4c5d"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("1f2a3b4c-5d6e-477f-8a9b-0c1d2e3f4a5b"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("2a3b4c5d-6e7f-488a-9b0c-1d2e3f4a5b6c"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("2c3d4e5f-6a7b-498c-9d0e-1f2a3b4c5d6e"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("3b4c5d6e-7f8a-499b-0c1d-2e3f4a5b6c7d"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("3d4e5f6a-7b8c-4a9d-0e1f-2a3b4c5d6e7f"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("4c5d6e7f-8a9b-4a0c-1d2e-3f4a5b6c7d8e"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("4e5f6a7b-8c9d-4b0e-1f2a-3b4c5d6e7f8a"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("5d6e7f8a-9b0c-4b1d-2e3f-4a5b6c7d8e9f"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("5f6a7b8c-9d0e-4c1f-2a3b-4c5d6e7f8a9b"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("6a7b8c9d-0e1f-4d2a-3b4c-5d6e7f8a9b0c"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("6e7f8a9b-0c1d-4c2e-3f4a-5b6c7d8e9f0a"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("7b8c9d0e-1f2a-4e3b-4c5d-6e7f8a9b0c1d"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("7f8a9b0c-1d2e-4d3f-4a5b-6c7d8e9f0a1b"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("8a9b0c1d-2e3f-4e4a-5b6c-7d8e9f0a1b2c"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("8c9d0e1f-2a3b-4f4c-5d6e-7f8a9b0c1d2e"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("9d0e1f2a-3b4c-455d-6e7f-8a9b0c1d2e3f"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-b2b2-c3c3-d4d4-e5e5e5e5e5e5"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("b1a2c3d4-e5f6-4789-9a0b-c1d2e3f4a5b6"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("b1b1b1b1-c2c2-d3d3-e4e4-f5f5f5f5f5f5"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("c1c1c1c1-d2d2-e3e3-f4f4-a5a5a5a5a5a5"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("c7d8e9f0-1a2b-4c5d-3e4f-5a6b7c8d9e0f"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("d1d1d1d1-e2e2-f3f3-a4a4-b5b5b5b5b5b5"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("d3e4f5a6-b7c8-4d9e-5f0a-1b2c3d4e5f6a"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("e1e1e1e1-f2f2-a3a3-b4b4-c5c5c5c5c5c5"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("e9f0a1b2-c3d4-4e5f-6a7b-8c9d0e1f2a3b"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("f1f1f1f1-a2a2-b3b3-c4c4-d5d5d5d5d5d5"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("f5a6b7c8-d9e0-4f1a-7b8c-9d0e1f2a3b4c"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("f5eea915-af74-46f2-e615-08dd5415a4a2"), new Guid("f5eea915-af74-46f2-e615-08dd5415a4d7") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("f5eea915-af74-46f2-e615-08dd5415a4a2"), new Guid("f5eea915-af74-46f2-e615-08dd5415a4d9") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("f5eea915-af74-46f2-e615-08dd5415a4a4"), new Guid("f5eea915-af74-46f2-e615-08dd5415a4d6") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("f5eea915-af74-46f2-e615-08dd5415a4a4"), new Guid("f5eea915-af74-46f2-e615-08dd5415a4d8") });

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("1a2b3c4d-e5f6-4c92-3031-323334353637"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("2b3c4d5e-f6a7-4d0a-3334-353637383940"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("3c4d5e6f-a7b8-4e1b-3637-383940414243"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("4d5e6f7a-b8c9-4f2c-3940-414243444546"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("5e6f7a8b-c9d0-4d3d-4243-444546474849"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("61237e1a-928b-494a-be63-d9b562a65896"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("6401b407-35d4-4fe6-a1ba-ecf945440fbf"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("6f7a8b9c-d1e2-4e4e-4546-474849505152"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("7a8b9c0d-e2f3-4f5f-4849-505152535455"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("8b9c0d1e-f3a4-416a-5152-535455565758"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("97d8a55f-ac24-4e02-87f7-48b2a101c0c1"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("9c0d1e2f-a4b5-4b7b-5455-565758596061"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-1011-121314151617"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("b7c8d9e0-f1a2-4b5c-1314-151617181920"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("bdb25542-aad6-41fc-9c5d-61debda238e0"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-4d9e-1617-181920212223"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("d5e6f7a8-b9c0-4e1f-2021-222324252627"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("e1f2a3b4-c5d6-4f7a-2324-252627282930"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("f7422ce9-5e89-4b49-08ca-08dd47669c80"));

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: new Guid("f9a0b1c2-d3e4-418b-2627-282930313233"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("b3f07752-2d97-4b2c-2949-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("f339b369-2a05-4eea-294d-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("75ae0504-974c-4ff2-ab13-c30374ac8558"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("ceb2b836-3b8c-4ea9-93cc-f5c442d5d966"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("d9123022-25c0-4493-b5eb-b11cfd829554"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f5eea915-af74-46f2-e615-08dd5415a4a2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f5eea915-af74-46f2-e615-08dd5415a4a4"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("28e17d69-9e0e-44a3-2947-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("3914316b-1b87-46f7-294f-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("601a0d84-0435-4221-294e-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("a914be7e-c545-4784-2948-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("b3f52184-3330-4750-294c-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("c9a9f0d0-1111-4444-8888-1234567890ab"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("d1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("e2f3a4b5-c6d7-8901-e2f3-a4b5c6d78901"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("e8a1a3e6-d8da-4928-294b-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("f15bd95c-8746-4236-294a-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("39ad172a-602e-4118-29d9-08dd398180c1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5e91fd72-53c3-43ee-3d87-08dd3498aaa5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("923043b5-e9c8-4ded-9cfa-08dd3e211f93"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f5eea915-af74-46f2-e615-08dd5415a4d6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f5eea915-af74-46f2-e615-08dd5415a4d7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f5eea915-af74-46f2-e615-08dd5415a4d8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f5eea915-af74-46f2-e615-08dd5415a4d9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f82bff97-d6d4-4d47-424e-08dd3e217c75"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("25eeca82-c189-4bbb-0209-08dd388dc7b9"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("e6554375-0932-462c-0207-08dd388dc7b9"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("fe33e674-6c47-49dc-c1f7-08dd46922384"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("45e0dcb1-62af-409a-8349-08dd4691b096"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7ca4b1aa-fa9c-40a2-5601-08dd46916b70"));
        }
    }
}
