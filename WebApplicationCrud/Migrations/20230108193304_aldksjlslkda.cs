using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class aldksjlslkda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisplayState",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeAdded",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TimesSold",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductInfoId",
                table: "OrderDetails",
                column: "ProductInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ProductInfos_ProductInfoId",
                table: "OrderDetails",
                column: "ProductInfoId",
                principalTable: "ProductInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ProductInfos_ProductInfoId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ProductInfoId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DisplayState",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TimeAdded",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TimesSold",
                table: "Products");
        }
    }
}
