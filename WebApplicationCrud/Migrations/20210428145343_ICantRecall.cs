using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class ICantRecall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_productid",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_productid",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductInfoid",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductInfoid",
                table: "Images",
                column: "ProductInfoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ProductInfos_ProductInfoid",
                table: "Images",
                column: "ProductInfoid",
                principalTable: "ProductInfos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ProductInfos_ProductInfoid",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProductInfoid",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductInfoid",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_productid",
                table: "Images",
                column: "productid");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_productid",
                table: "Images",
                column: "productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
