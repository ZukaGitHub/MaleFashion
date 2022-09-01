using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class CommentsAnothreUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "SubComments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "SubComments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "SubComments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "MainComment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "MainComment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "MainComment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "MainComment");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "MainComment");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "MainComment");
        }
    }
}
