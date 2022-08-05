using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class esra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thumbnails_Products_Productid",
                table: "Thumbnails");

            migrationBuilder.RenameColumn(
                name: "Productid",
                table: "Thumbnails",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Thumbnails_Productid",
                table: "Thumbnails",
                newName: "IX_Thumbnails_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Thumbnails",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Thumbnails_Products_ProductId",
                table: "Thumbnails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thumbnails_Products_ProductId",
                table: "Thumbnails");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Thumbnails",
                newName: "Productid");

            migrationBuilder.RenameIndex(
                name: "IX_Thumbnails_ProductId",
                table: "Thumbnails",
                newName: "IX_Thumbnails_Productid");

            migrationBuilder.AlterColumn<int>(
                name: "Productid",
                table: "Thumbnails",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Thumbnails_Products_Productid",
                table: "Thumbnails",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
