using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class ProductInfStockAndSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockOnHolds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductInfoStockAndSizeId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOnHolds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockOnHolds_ProductInfoStockAndSize_ProductInfoStockAndSizeId",
                        column: x => x.ProductInfoStockAndSizeId,
                        principalTable: "ProductInfoStockAndSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockOnHolds_ProductInfoStockAndSizeId",
                table: "StockOnHolds",
                column: "ProductInfoStockAndSizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockOnHolds");
        }
    }
}
