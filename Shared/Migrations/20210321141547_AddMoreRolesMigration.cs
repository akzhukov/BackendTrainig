using Microsoft.EntityFrameworkCore.Migrations;

namespace Shared.Migrations
{
    public partial class AddMoreRolesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "307fc523-5d93-49db-95b2-8d6b7fadba4a", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2", "e4a725fb-4c68-412a-a516-ebe63edda099", "Manager", null },
                    { "3", "75d1207c-b1c3-4542-b189-ba998653b2bf", "User", null }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e49297eb-08ef-47e0-8a83-ce03c748be9d", "AQAAAAEAACcQAAAAEKflwb2HZceOEuNIKEEsmRd2E4Vx2Pu6FFqVpDZBGqK6pSmiCAfceGlZgT1u+z5Ygw==", "dcc789b4-90ca-4038-84c0-acba7b60c141" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "210513dc-188c-4de8-902c-95e0e5fe34fd", "administrator" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b2b7d6c-68fb-4fe7-b9c7-99db7a91bf80", "AQAAAAEAACcQAAAAELRBinXUVnNAgNh3s17dDVb96MH+FP2Ay67PFZ7ZBt2233crx0zQWzs8ytu8U8iY2w==", "b75e41a7-7650-4376-a997-266b633a5d1c" });
        }
    }
}
