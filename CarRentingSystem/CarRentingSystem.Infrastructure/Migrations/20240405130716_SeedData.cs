using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentingSystem.Infrastructure.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Audi" },
                    { 4, "Toyota" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Sedan" },
                    { 6, "Luxury" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandId", "CategoryId", "Color", "DealerId", "Description", "FuelType", "GearType", "ImageUrl", "IsApproved", "Price", "RenterId", "Year" },
                values: new object[] { 3, 3, 6, "black", 1, "The car has no complaints", 1, 2, "https://i.ytimg.com/vi/gxaUwYHMqpE/maxresdefault.jpg", false, 4500m, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
