using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class CommentsProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Productid",
                table: "MainComment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainComment_Productid",
                table: "MainComment",
                column: "Productid");

            migrationBuilder.AddForeignKey(
                name: "FK_MainComment_Products_Productid",
                table: "MainComment",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainComment_Products_Productid",
                table: "MainComment");

            migrationBuilder.DropIndex(
                name: "IX_MainComment_Productid",
                table: "MainComment");

            migrationBuilder.DropColumn(
                name: "Productid",
                table: "MainComment");
        }
    }
}
