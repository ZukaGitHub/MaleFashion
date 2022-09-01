using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class PostUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Qoute",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QouteAuthor",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Qoute",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "QouteAuthor",
                table: "Posts");
        }
    }
}
