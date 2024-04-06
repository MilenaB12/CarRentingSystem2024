using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentingSystem.Infrastructure.Migrations
{
    public partial class MoreSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "24a2453b-bfea-3abe-4b2a-beabf3525a21", 0, "8502d69a-e1e9-4898-aabe-0aae786bee3e", "Simona@abv", false, "Simona", "Hristova", false, null, "Simona@abv", "Simona@abv", "AQAAAAIAAYagAAAAED927eL5c/CJwByyx7BQ9gYAAzv6sXcDfpGP9iewpZtvUVZpo5c6mhbPrk/7zsr+LA==", null, false, "68fb8095-5488-4f9c-9715-b42b994e116f", false, "Simona@abv" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a56ec564-782b-6351-da53-81a4b53acaf2", 0, "f954559c-b165-483d-966b-ac03966c95a7", "dealer@abv", false, "Mitko", "Dimitrov", false, null, "dealer@abv", "dealer@abv", "AQAAAAIAAYagAAAAEDV/W+i6GDr9ToePyLLBktejpACt1YPrHYCC3HIrTG/VSsRwCuI2CKvJMW6hProGrw==", null, false, "53eb4192-18e5-4d4e-be7e-a7cd3fd6c404", false, "dealer@abv" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 4, "user:fullname", "Mitko Dimitrov", "a56ec564-782b-6351-da53-81a4b53acaf2" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 5, "user:fullname", "Simona Hristova", "24a2453b-bfea-3abe-4b2a-beabf3525a21" });

            migrationBuilder.InsertData(
                table: "Dealers",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { 2, "+359676767676", "a56ec564-782b-6351-da53-81a4b53acaf2" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandId", "CategoryId", "Color", "DealerId", "Description", "FuelType", "GearType", "ImageUrl", "IsApproved", "Price", "RenterId", "Year" },
                values: new object[] { 4, 4, 5, "grey", 2, "Whether you're headed out of town for a vacation, need a vehicle for business in a new city, have your current car in the shop, or are looking to experience an extended test drive before purchase, you can rely on a Toyota car rental.", 3, 2, "https://mobistatic4.focus.bg/mobile/photosorg/821/1/big//11690282183094821_4k.jpg", false, 4200m, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24a2453b-bfea-3abe-4b2a-beabf3525a21");

            migrationBuilder.DeleteData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a56ec564-782b-6351-da53-81a4b53acaf2");
        }
    }
}
