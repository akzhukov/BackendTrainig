using Microsoft.EntityFrameworkCore.Migrations;

namespace Shared.Migrations
{
    public partial class AddAdminUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b766db85-1685-4560-8fb0-ac04e42528f6", 0, "9f2f91b7-619d-4b26-9f12-07457f58ae22", null, false, false, null, null, null, "AQAAAAEAACcQAAAAEP4b1ryWyT8Khzm8ym8qO4A43Aov4tgBCZxEFn2Tq89lSEOmFqGj8cwGtU9jepCUWQ==", null, false, "fe30481f-f804-4f93-bab3-bf015c0a35c1", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b766db85-1685-4560-8fb0-ac04e42528f6");
        }
    }
}
