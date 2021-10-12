using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreManagement.Infrastructure.EfCore.Migrations
{
    public partial class ChangeOrderItemsProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderItems",
                newName: "PayAmount");

            migrationBuilder.RenameColumn(
                name: "DiscountRate",
                table: "OrderItems",
                newName: "Status");

            migrationBuilder.AddColumn<double>(
                name: "DiscountPrice",
                table: "OrderItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "OrderItems",
                newName: "DiscountRate");

            migrationBuilder.RenameColumn(
                name: "PayAmount",
                table: "OrderItems",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
