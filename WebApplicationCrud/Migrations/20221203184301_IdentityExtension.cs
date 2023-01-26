using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class IdentityExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryInfoId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    AdditionalDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DeliveryInfoId",
                table: "AspNetUsers",
                column: "DeliveryInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DeliveryInfos_DeliveryInfoId",
                table: "AspNetUsers",
                column: "DeliveryInfoId",
                principalTable: "DeliveryInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DeliveryInfos_DeliveryInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DeliveryInfos");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DeliveryInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeliveryInfoId",
                table: "AspNetUsers");
        }
    }
}
