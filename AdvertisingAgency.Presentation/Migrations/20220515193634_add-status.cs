using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertisingAgency.Presentation.Migrations
{
    public partial class addstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFastOrder",
                table: "FavorOrders",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFastOrder",
                table: "FavorOrders");
        }
    }
}
