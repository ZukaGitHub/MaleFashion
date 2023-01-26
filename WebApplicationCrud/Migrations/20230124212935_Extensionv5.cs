using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class Extensionv5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SizeText",
                table: "shoppingCartItems",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ProductInfoStockAndSize",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeText",
                table: "shoppingCartItems");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ProductInfoStockAndSize");
        }
    }
}
