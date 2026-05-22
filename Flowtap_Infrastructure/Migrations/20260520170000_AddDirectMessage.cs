using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Flowtap_Infrastructure.Persistence.DbContext;

#nullable disable

namespace Flowtap_Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260520170000_AddDirectMessage")]
    public partial class AddDirectMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DirectMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Subject = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Body = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsComplaint = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeletedBySender = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeletedByRecipient = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectMessages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_CompanyId",
                table: "DirectMessages",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_RecipientId",
                table: "DirectMessages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_SenderId",
                table: "DirectMessages",
                column: "SenderId");

            // Composite index for inbox query performance
            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_RecipientId_IsRead",
                table: "DirectMessages",
                columns: new[] { "RecipientId", "IsRead" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "DirectMessages");
        }
    }
}
