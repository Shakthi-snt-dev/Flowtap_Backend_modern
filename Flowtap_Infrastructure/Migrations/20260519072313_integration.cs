using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowtap_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class integration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<Guid>(
                name: "CashierEmployeeId",
                table: "Sales",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Employees",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalaryCurrency",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalaryType",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Integrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Provider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ConfigJson = table.Column<string>(type: "text", nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    ConnectedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WebhookUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LastStatusMessage = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    LastCheckedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Integrations_Tenants_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Integrations_CompanyId",
                table: "Integrations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Integrations_CompanyId_Provider",
                table: "Integrations",
                columns: new[] { "CompanyId", "Provider" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Integrations");

            migrationBuilder.DropColumn(
                name: "LocationCode",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CashierEmployeeId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SalaryCurrency",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SalaryType",
                table: "Employees");
        }
    }
}
