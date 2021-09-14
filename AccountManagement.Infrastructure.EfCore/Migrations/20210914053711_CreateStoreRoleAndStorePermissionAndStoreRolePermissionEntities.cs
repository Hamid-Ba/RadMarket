using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Infrastructure.EfCore.Migrations
{
    public partial class CreateStoreRoleAndStorePermissionAndStoreRolePermissionEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StoreRoleId",
                table: "StoreUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "StorePermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorePermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreRolePermissions",
                columns: table => new
                {
                    StoreRoleId = table.Column<long>(type: "bigint", nullable: false),
                    StorePermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreRolePermissions", x => new { x.StoreRoleId, x.StorePermissionId });
                    table.ForeignKey(
                        name: "FK_StoreRolePermissions_StorePermissions_StorePermissionId",
                        column: x => x.StorePermissionId,
                        principalTable: "StorePermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreRolePermissions_StoreRole_StoreRoleId",
                        column: x => x.StoreRoleId,
                        principalTable: "StoreRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreUser_StoreRoleId",
                table: "StoreUser",
                column: "StoreRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRolePermissions_StorePermissionId",
                table: "StoreRolePermissions",
                column: "StorePermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreUser_StoreRole_StoreRoleId",
                table: "StoreUser",
                column: "StoreRoleId",
                principalTable: "StoreRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreUser_StoreRole_StoreRoleId",
                table: "StoreUser");

            migrationBuilder.DropTable(
                name: "StoreRolePermissions");

            migrationBuilder.DropTable(
                name: "StorePermissions");

            migrationBuilder.DropTable(
                name: "StoreRole");

            migrationBuilder.DropIndex(
                name: "IX_StoreUser_StoreRoleId",
                table: "StoreUser");

            migrationBuilder.DropColumn(
                name: "StoreRoleId",
                table: "StoreUser");
        }
    }
}
