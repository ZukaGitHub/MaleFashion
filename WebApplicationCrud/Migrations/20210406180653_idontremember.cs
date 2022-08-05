using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class idontremember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalePercentage",
                table: "ProductInfos");

            migrationBuilder.AddColumn<float>(
                name: "NewPrice",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalePercentage",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalePercentage",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "SalePercentage",
                table: "ProductInfos",
                nullable: true);
        }
    }
}
