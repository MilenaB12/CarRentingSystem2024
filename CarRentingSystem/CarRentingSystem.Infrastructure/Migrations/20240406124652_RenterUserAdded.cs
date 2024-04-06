using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentingSystem.Infrastructure.Migrations
{
    public partial class RenterUserAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dealers_UserId",
                table: "Dealers");

            migrationBuilder.AlterColumn<string>(
                name: "RenterId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24a2453b-bfea-3abe-4b2a-beabf3525a21",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a999d5f-35a9-4ef3-87e8-75160aa4d81e", "AQAAAAIAAYagAAAAEC7vw2fLnhjpSPA5x2VhnFRz1MMdSV3yahZ9ctLeJq4i73M76MaJYQ6OlELyAmJ1Pw==", "8e107279-cba1-4504-a4be-6246705c8cbd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a56ec564-782b-6351-da53-81a4b53acaf2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1eda1b5-66bb-4995-892e-f15c3c94089e", "AQAAAAIAAYagAAAAECieUa19wf10GfehJSvHmvqj+ms/CSVrHhJLSCk1yK3pA6sPw3XSzPZNLJeiLPqVUQ==", "f5fd8f81-24b2-4007-9268-fe60876c1188" });

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_UserId",
                table: "Dealers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RenterId",
                table: "Cars",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_RenterId",
                table: "Cars",
                column: "RenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_RenterId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Dealers_UserId",
                table: "Dealers");

            migrationBuilder.DropIndex(
                name: "IX_Cars_RenterId",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "RenterId",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24a2453b-bfea-3abe-4b2a-beabf3525a21",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8502d69a-e1e9-4898-aabe-0aae786bee3e", "AQAAAAIAAYagAAAAED927eL5c/CJwByyx7BQ9gYAAzv6sXcDfpGP9iewpZtvUVZpo5c6mhbPrk/7zsr+LA==", "68fb8095-5488-4f9c-9715-b42b994e116f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a56ec564-782b-6351-da53-81a4b53acaf2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f954559c-b165-483d-966b-ac03966c95a7", "AQAAAAIAAYagAAAAEDV/W+i6GDr9ToePyLLBktejpACt1YPrHYCC3HIrTG/VSsRwCuI2CKvJMW6hProGrw==", "53eb4192-18e5-4d4e-be7e-a7cd3fd6c404" });

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_UserId",
                table: "Dealers",
                column: "UserId");
        }
    }
}
