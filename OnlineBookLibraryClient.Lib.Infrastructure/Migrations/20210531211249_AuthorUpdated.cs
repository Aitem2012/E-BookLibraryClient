using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBookLibraryClient.Lib.Infrastructure.Migrations
{
    public partial class AuthorUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorName",
                table: "Authors",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Authors",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Authors",
                newName: "AuthorName");
        }
    }
}
