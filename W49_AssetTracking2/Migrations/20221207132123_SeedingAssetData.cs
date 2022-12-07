using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace W49AssetTracking2.Migrations
{
    /// <inheritdoc />
    public partial class SeedingAssetData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "Brand", "DateOfPurchase", "Model", "OfficeId", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "Lenovo", new DateTime(2019, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ideacentre", 2, 1530, "Computer" },
                    { 2, "HP", new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Z2 Tower", 3, 1490, "Computer" },
                    { 3, "Lenovo", new DateTime(2022, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "ThinkPad X1", 3, 580, "Laptop" },
                    { 4, "Samsung", new DateTime(2020, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Galaxy", 1, 550, "Laptop" },
                    { 5, "Samsung", new DateTime(2022, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "S20", 2, 180, "Phone" },
                    { 6, "Apple", new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "iPhone 10", 1, 250, "Phone" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
