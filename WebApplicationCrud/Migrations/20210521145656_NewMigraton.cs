using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class NewMigraton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thumbnails_Products_ProductId",
                table: "Thumbnails");

            migrationBuilder.DropIndex(
                name: "IX_Thumbnails_ProductId",
                table: "Thumbnails");

            migrationBuilder.AddColumn<int>(
                name: "ProductInfoid",
                table: "Thumbnails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_ProductInfoid",
                table: "Thumbnails",
                column: "ProductInfoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Thumbnails_ProductInfos_ProductInfoid",
                table: "Thumbnails",
                column: "ProductInfoid",
                principalTable: "ProductInfos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thumbnails_ProductInfos_ProductInfoid",
                table: "Thumbnails");

            migrationBuilder.DropIndex(
                name: "IX_Thumbnails_ProductInfoid",
                table: "Thumbnails");

            migrationBuilder.DropColumn(
                name: "ProductInfoid",
                table: "Thumbnails");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_ProductId",
                table: "Thumbnails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Thumbnails_Products_ProductId",
                table: "Thumbnails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
