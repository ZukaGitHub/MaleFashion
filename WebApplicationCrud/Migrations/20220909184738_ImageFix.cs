using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class ImageFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailIndex",
                table: "ProductInfos");

            migrationBuilder.AddColumn<string>(
                name: "ProductInfoThumbnailName",
                table: "ProductInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductInfoThumbnailName",
                table: "ProductInfos");

            migrationBuilder.AddColumn<int>(
                name: "ThumbnailIndex",
                table: "ProductInfos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
