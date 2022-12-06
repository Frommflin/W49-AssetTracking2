using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace W49AssetTracking2.Migrations
{
    /// <inheritdoc />
    public partial class SeedingHardwareData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hardwares",
                columns: new[] { "Id", "Brand", "DateOfPurchase", "Model", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "Lenovo", new DateTime(2021, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Legion", 1600, "Computer" },
                    { 2, "Acer", new DateTime(2019, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspire XC", 1550, "Computer" },
                    { 3, "Apple", new DateTime(2021, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "MacBook", 650, "Laptop" },
                    { 4, "Samsung", new DateTime(2020, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Galaxy", 550, "Laptop" },
                    { 5, "Samsung", new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "S21", 200, "Phone" },
                    { 6, "Huawei", new DateTime(2019, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "P30 Pro", 150, "Phone" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hardwares",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hardwares",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hardwares",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hardwares",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hardwares",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hardwares",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
