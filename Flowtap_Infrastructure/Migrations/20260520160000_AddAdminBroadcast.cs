using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Flowtap_Infrastructure.Persistence.DbContext;

#nullable disable

namespace Flowtap_Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260520160000_AddAdminBroadcast")]
    /// <inheritdoc />
    public partial class AddAdminBroadcast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminBroadcasts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    Subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Severity = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Information"),
                    SentBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValue: "Admin"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminBroadcasts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminBroadcasts_CompanyId",
                table: "AdminBroadcasts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminBroadcasts_CompanyId_IsActive",
                table: "AdminBroadcasts",
                columns: new[] { "CompanyId", "IsActive" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AdminBroadcasts");
        }
    }
}
