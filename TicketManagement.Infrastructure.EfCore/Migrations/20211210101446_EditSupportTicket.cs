using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketManagement.Infrastructure.EfCore.Migrations
{
    public partial class EditSupportTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreTicketMessage_StoreTickets_StoreTicketId",
                table: "StoreTicketMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessage_Tickets_TicketId",
                table: "TicketMessage");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TicketMessage",
                newName: "SenderId");

            migrationBuilder.AddColumn<long>(
                name: "ReciverId",
                table: "TicketMessage",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreTicketMessage_StoreTickets_StoreTicketId",
                table: "StoreTicketMessage",
                column: "StoreTicketId",
                principalTable: "StoreTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessage_Tickets_TicketId",
                table: "TicketMessage",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreTicketMessage_StoreTickets_StoreTicketId",
                table: "StoreTicketMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessage_Tickets_TicketId",
                table: "TicketMessage");

            migrationBuilder.DropColumn(
                name: "ReciverId",
                table: "TicketMessage");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "TicketMessage",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreTicketMessage_StoreTickets_StoreTicketId",
                table: "StoreTicketMessage",
                column: "StoreTicketId",
                principalTable: "StoreTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessage_Tickets_TicketId",
                table: "TicketMessage",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
