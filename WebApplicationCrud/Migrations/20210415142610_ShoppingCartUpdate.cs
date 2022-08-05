using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class ShoppingCartUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCartItems_ProductInfos_ProductInfoIdid",
                table: "shoppingCartItems");

            migrationBuilder.RenameColumn(
                name: "ProductInfoIdid",
                table: "shoppingCartItems",
                newName: "ProductInfoid");

            migrationBuilder.RenameIndex(
                name: "IX_shoppingCartItems_ProductInfoIdid",
                table: "shoppingCartItems",
                newName: "IX_shoppingCartItems_ProductInfoid");

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCartItems_ProductInfos_ProductInfoid",
                table: "shoppingCartItems",
                column: "ProductInfoid",
                principalTable: "ProductInfos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCartItems_ProductInfos_ProductInfoid",
                table: "shoppingCartItems");

            migrationBuilder.RenameColumn(
                name: "ProductInfoid",
                table: "shoppingCartItems",
                newName: "ProductInfoIdid");

            migrationBuilder.RenameIndex(
                name: "IX_shoppingCartItems_ProductInfoid",
                table: "shoppingCartItems",
                newName: "IX_shoppingCartItems_ProductInfoIdid");

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCartItems_ProductInfos_ProductInfoIdid",
                table: "shoppingCartItems",
                column: "ProductInfoIdid",
                principalTable: "ProductInfos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
