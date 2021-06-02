using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBookLibraryClient.Lib.Infrastructure.Migrations
{
    public partial class ReviewUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "Ratings",
                table: "Reviews",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "58f6e36d-6210-49e8-a1ea-4a33f9e0f3e2", "26811a80-49f0-4317-9415-231adf2061b7", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Photo", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "04fb0a13-4ff0-4b79-8460-eea8a9e0242a", 0, "b63f00f4-c99d-463e-af49-d4d82373de9c", "admin@gmail.com", false, "Ayobami", "Fadeni", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEF9Jn9Vs+Bb1UGkdLoeqg41/dAUYfGV5/2v74kaYqLBOpvBxvLifGZ9QsmQS5ZgNTw==", null, false, null, "4ebdd888-0164-450d-ba7a-35093cb00a05", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "58f6e36d-6210-49e8-a1ea-4a33f9e0f3e2", "04fb0a13-4ff0-4b79-8460-eea8a9e0242a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "58f6e36d-6210-49e8-a1ea-4a33f9e0f3e2", "04fb0a13-4ff0-4b79-8460-eea8a9e0242a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58f6e36d-6210-49e8-a1ea-4a33f9e0f3e2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "04fb0a13-4ff0-4b79-8460-eea8a9e0242a");

            migrationBuilder.DropColumn(
                name: "Ratings",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
    }
}
