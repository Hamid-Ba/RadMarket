using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketManagement.Infrastructure.EfCore.Migrations
{
    public partial class CreateTicketEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreTickets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: false),
                    FirstStoreId = table.Column<long>(type: "bigint", nullable: false),
                    SecondStoreId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreTickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreTicketMessage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreTicketId = table.Column<long>(type: "bigint", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    ReciverId = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreTicketMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreTicketMessage_StoreTickets_StoreTicketId",
                        column: x => x.StoreTicketId,
                        principalTable: "StoreTickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreTicketMessage_StoreTicketId",
                table: "StoreTicketMessage",
                column: "StoreTicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreTicketMessage");

            migrationBuilder.DropTable(
                name: "StoreTickets");
        }
    }
}
