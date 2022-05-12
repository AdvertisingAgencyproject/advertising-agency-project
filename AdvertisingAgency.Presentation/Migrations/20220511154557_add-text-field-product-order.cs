using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertisingAgency.Presentation.Migrations
{
    public partial class addtextfieldproductorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "ProductOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "ProductOrders");
        }
    }
}
