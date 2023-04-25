using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "CreateDate", "CustomerEmail", "Status", "TotalPrice" },
                values: new object[] { 1, new DateTime(2023, 4, 25, 21, 14, 28, 29, DateTimeKind.Local).AddTicks(9098), "client1@gmail.com", 1, 11.98m });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "IsDeleted", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, false, "Product 1", 1.99m, (short)10 },
                    { 2, false, "Product 2", 123.50m, (short)100 }
                });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "OrderId", "Price", "ProductId", "Quantity", "TotalPrice" },
                values: new object[,]
                {
                    { 1, 1, 0.99m, 1, (short)2, 1.98m },
                    { 2, 1, 2m, 2, (short)5, 10m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
