using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class ProductInfoVueUpdateadminPanel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ProductInfos");

            migrationBuilder.DropColumn(
                name: "SizeText",
                table: "ProductInfos");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ProductInfos",
                newName: "ThumbnailIndex");

            migrationBuilder.CreateTable(
                name: "ProductInfoStockAndSize",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductInfoId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SizeName = table.Column<string>(nullable: true),
                    stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInfoStockAndSize", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductInfoStockAndSize_ProductInfos_ProductInfoId",
                        column: x => x.ProductInfoId,
                        principalTable: "ProductInfos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfoStockAndSize_ProductInfoId",
                table: "ProductInfoStockAndSize",
                column: "ProductInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInfoStockAndSize");

            migrationBuilder.RenameColumn(
                name: "ThumbnailIndex",
                table: "ProductInfos",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "ProductInfoid",
                table: "Thumbnails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "ProductInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeText",
                table: "ProductInfos",
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
    }
}
