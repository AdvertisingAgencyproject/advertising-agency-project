using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertisingAgency.Presentation.Migrations
{
    public partial class editprodorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "ProductOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "ProductOrders");
        }
    }
}
