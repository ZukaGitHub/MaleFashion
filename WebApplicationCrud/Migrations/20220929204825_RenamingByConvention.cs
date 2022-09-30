using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationCrud.Migrations
{
    public partial class RenamingByConvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ProductInfos_ProductInfoid",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_MainComment_Products_Productid",
                table: "MainComment");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_brandid",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCartItems_ProductInfos_ProductInfoid",
                table: "shoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCartItems_Products_Productid",
                table: "shoppingCartItems");

            migrationBuilder.DropColumn(
                name: "stock",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "address1",
                table: "UserInfos",
                newName: "Address1");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Thumbnails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "TextSizes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TextSizes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Productid",
                table: "shoppingCartItems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductInfoid",
                table: "shoppingCartItems",
                newName: "ProductInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_shoppingCartItems_Productid",
                table: "shoppingCartItems",
                newName: "IX_shoppingCartItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_shoppingCartItems_ProductInfoid",
                table: "shoppingCartItems",
                newName: "IX_shoppingCartItems_ProductInfoId");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "brandid",
                table: "Products",
                newName: "BrandId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "desc",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Products_brandid",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "ProductInfoStockAndSize",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProductInfoStockAndSize",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "color",
                table: "ProductInfos",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProductInfos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "color",
                table: "OrderDetails",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OrderDetails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Productid",
                table: "MainComment",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_MainComment_Productid",
                table: "MainComment",
                newName: "IX_MainComment_ProductId");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "Images",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductInfoid",
                table: "Images",
                newName: "ProductInfoId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Images",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ProductInfoid",
                table: "Images",
                newName: "IX_Images_ProductInfoId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Brands",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Brands",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ProductInfos_ProductInfoId",
                table: "Images",
                column: "ProductInfoId",
                principalTable: "ProductInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MainComment_Products_ProductId",
                table: "MainComment",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCartItems_Products_ProductId",
                table: "shoppingCartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCartItems_ProductInfos_ProductInfoId",
                table: "shoppingCartItems",
                column: "ProductInfoId",
                principalTable: "ProductInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_ProductInfos_ProductInfoId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_MainComment_Products_ProductId",
                table: "MainComment");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCartItems_Products_ProductId",
                table: "shoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCartItems_ProductInfos_ProductInfoId",
                table: "shoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProductId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "UserInfos",
                newName: "address1");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Thumbnails",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TextSizes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TextSizes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProductInfoId",
                table: "shoppingCartItems",
                newName: "ProductInfoid");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "shoppingCartItems",
                newName: "Productid");

            migrationBuilder.RenameIndex(
                name: "IX_shoppingCartItems_ProductInfoId",
                table: "shoppingCartItems",
                newName: "IX_shoppingCartItems_ProductInfoid");

            migrationBuilder.RenameIndex(
                name: "IX_shoppingCartItems_ProductId",
                table: "shoppingCartItems",
                newName: "IX_shoppingCartItems_Productid");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Products",
                newName: "brandid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "desc");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                newName: "IX_Products_brandid");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "ProductInfoStockAndSize",
                newName: "stock");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductInfoStockAndSize",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "ProductInfos",
                newName: "color");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductInfos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "OrderDetails",
                newName: "color");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderDetails",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "MainComment",
                newName: "Productid");

            migrationBuilder.RenameIndex(
                name: "IX_MainComment_ProductId",
                table: "MainComment",
                newName: "IX_MainComment_Productid");

            migrationBuilder.RenameColumn(
                name: "ProductInfoId",
                table: "Images",
                newName: "ProductInfoid");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Images",
                newName: "productid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Images",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ProductInfoId",
                table: "Images",
                newName: "IX_Images_ProductInfoid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Brands",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Brands",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "stock",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ProductInfos_ProductInfoid",
                table: "Images",
                column: "ProductInfoid",
                principalTable: "ProductInfos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MainComment_Products_Productid",
                table: "MainComment",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_brandid",
                table: "Products",
                column: "brandid",
                principalTable: "Brands",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCartItems_ProductInfos_ProductInfoid",
                table: "shoppingCartItems",
                column: "ProductInfoid",
                principalTable: "ProductInfos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCartItems_Products_Productid",
                table: "shoppingCartItems",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
