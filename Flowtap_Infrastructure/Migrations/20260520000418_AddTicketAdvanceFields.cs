using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowtap_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketAdvanceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Financials_PrepaymentMethod",
                table: "ServiceTickets",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Financials_PrepaymentPaidAt",
                table: "ServiceTickets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TicketId",
                table: "Payments",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_TicketId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Financials_PrepaymentMethod",
                table: "ServiceTickets");

            migrationBuilder.DropColumn(
                name: "Financials_PrepaymentPaidAt",
                table: "ServiceTickets");
        }
    }
}
