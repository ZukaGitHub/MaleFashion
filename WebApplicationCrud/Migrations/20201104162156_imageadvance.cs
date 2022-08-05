using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class imageadvance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_Productid",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Productid",
                table: "Images",
                newName: "productid");

            migrationBuilder.RenameIndex(
                name: "IX_Images_Productid",
                table: "Images",
                newName: "IX_Images_productid");

            migrationBuilder.AlterColumn<int>(
                name: "productid",
                table: "Images",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_productid",
                table: "Images",
                column: "productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_productid",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "Images",
                newName: "Productid");

            migrationBuilder.RenameIndex(
                name: "IX_Images_productid",
                table: "Images",
                newName: "IX_Images_Productid");

            migrationBuilder.AlterColumn<int>(
                name: "Productid",
                table: "Images",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_Productid",
                table: "Images",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
