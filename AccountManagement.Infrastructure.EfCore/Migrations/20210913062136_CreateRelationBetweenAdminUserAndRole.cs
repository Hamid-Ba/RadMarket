using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Infrastructure.EfCore.Migrations
{
    public partial class CreateRelationBetweenAdminUserAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AdminRoleId",
                table: "AdminUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AdminRolePermissions",
                columns: table => new
                {
                    AdminRoleId = table.Column<long>(type: "bigint", nullable: false),
                    AdminPermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRolePermissions", x => new { x.AdminRoleId, x.AdminPermissionId });
                    table.ForeignKey(
                        name: "FK_AdminRolePermissions_AdminPermissions_AdminPermissionId",
                        column: x => x.AdminPermissionId,
                        principalTable: "AdminPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminRolePermissions_AdminRoles_AdminRoleId",
                        column: x => x.AdminRoleId,
                        principalTable: "AdminRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_AdminRoleId",
                table: "AdminUsers",
                column: "AdminRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminRolePermissions_AdminPermissionId",
                table: "AdminRolePermissions",
                column: "AdminPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_AdminRoles_AdminRoleId",
                table: "AdminUsers",
                column: "AdminRoleId",
                principalTable: "AdminRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_AdminRoles_AdminRoleId",
                table: "AdminUsers");

            migrationBuilder.DropTable(
                name: "AdminRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_AdminUsers_AdminRoleId",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "AdminRoleId",
                table: "AdminUsers");
        }
    }
}
