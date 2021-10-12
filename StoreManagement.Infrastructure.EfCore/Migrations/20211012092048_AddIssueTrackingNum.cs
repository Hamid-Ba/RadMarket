using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreManagement.Infrastructure.EfCore.Migrations
{
    public partial class AddIssueTrackingNum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IssueTracking",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueTracking",
                table: "Orders");
        }
    }
}
