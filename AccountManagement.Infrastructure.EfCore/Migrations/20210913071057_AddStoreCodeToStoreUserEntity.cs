using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Infrastructure.EfCore.Migrations
{
    public partial class AddStoreCodeToStoreUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoreCode",
                table: "StoreUser",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreCode",
                table: "StoreUser");
        }
    }
}
