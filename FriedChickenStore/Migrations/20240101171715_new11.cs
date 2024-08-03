using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FriedChickenStore.Migrations
{
    public partial class new11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "price",
                table: "OrderDetails");
        }
    }
}
