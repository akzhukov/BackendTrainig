using Microsoft.EntityFrameworkCore.Migrations;

namespace Shared.Migrations
{
    public partial class AddRolesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b766db85-1685-4560-8fb0-ac04e42528f6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "210513dc-188c-4de8-902c-95e0e5fe34fd", "administrator", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "7b2b7d6c-68fb-4fe7-b9c7-99db7a91bf80", null, false, false, null, null, null, "AQAAAAEAACcQAAAAELRBinXUVnNAgNh3s17dDVb96MH+FP2Ay67PFZ7ZBt2233crx0zQWzs8ytu8U8iY2w==", null, false, "b75e41a7-7650-4376-a997-266b633a5d1c", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b766db85-1685-4560-8fb0-ac04e42528f6", 0, "9f2f91b7-619d-4b26-9f12-07457f58ae22", null, false, false, null, null, null, "AQAAAAEAACcQAAAAEP4b1ryWyT8Khzm8ym8qO4A43Aov4tgBCZxEFn2Tq89lSEOmFqGj8cwGtU9jepCUWQ==", null, false, "fe30481f-f804-4f93-bab3-bf015c0a35c1", false, "admin" });
        }
    }
}
