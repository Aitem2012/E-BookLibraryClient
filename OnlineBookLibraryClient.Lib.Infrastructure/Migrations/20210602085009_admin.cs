using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBookLibraryClient.Lib.Infrastructure.Migrations
{
    public partial class admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2cff6d34-a6d7-46f8-b6ff-0da0ec178d51", "7173ee23-5b86-4545-9c60-419f7d45cca1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Photo", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "df75366f-944e-4f6f-a31c-031bd053c7d6", 0, "4aaaa8bb-732d-48e4-8422-52074f6bc00c", "admin@gmail.com", false, "Ayobami", "Fadeni", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEDrEuW5htlI5hr+/PTLpgJ0HiWz8pxMswzouC66YQ8Zc+8MsdAce1AjxAZnq35uA3g==", null, false, null, "3424a0ac-7555-4d37-9fe3-c4f939940072", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2cff6d34-a6d7-46f8-b6ff-0da0ec178d51", "df75366f-944e-4f6f-a31c-031bd053c7d6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2cff6d34-a6d7-46f8-b6ff-0da0ec178d51", "df75366f-944e-4f6f-a31c-031bd053c7d6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2cff6d34-a6d7-46f8-b6ff-0da0ec178d51");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df75366f-944e-4f6f-a31c-031bd053c7d6");
        }
    }
}
