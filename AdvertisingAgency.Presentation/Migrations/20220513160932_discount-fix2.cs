using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertisingAgency.Presentation.Migrations
{
    public partial class discountfix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Products_ProductId",
                table: "Discounts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Discounts",
                newName: "FavorId");

            migrationBuilder.RenameIndex(
                name: "IX_Discounts_ProductId",
                table: "Discounts",
                newName: "IX_Discounts_FavorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Favors_FavorId",
                table: "Discounts",
                column: "FavorId",
                principalTable: "Favors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Favors_FavorId",
                table: "Discounts");

            migrationBuilder.RenameColumn(
                name: "FavorId",
                table: "Discounts",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Discounts_FavorId",
                table: "Discounts",
                newName: "IX_Discounts_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Products_ProductId",
                table: "Discounts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
