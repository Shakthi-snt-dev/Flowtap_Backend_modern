using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowtap_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThemeMode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "light"),
                    AccentColor = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "blue"),
                    FontFamily = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValue: "inter"),
                    BorderRadius = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "normal"),
                    SidebarDensity = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "comfortable"),
                    RequireClientOnSale = table.Column<bool>(type: "boolean", nullable: false),
                    AllowDiscount = table.Column<bool>(type: "boolean", nullable: false),
                    MaxDiscountPercent = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    AllowVoid = table.Column<bool>(type: "boolean", nullable: false),
                    RequireManagerPinForVoid = table.Column<bool>(type: "boolean", nullable: false),
                    AutoPrintReceipt = table.Column<bool>(type: "boolean", nullable: false),
                    ReceiptFooterText = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    OpeningTime = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    ClosingTime = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreSettings_CompanyId",
                table: "StoreSettings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreSettings_LocationId",
                table: "StoreSettings",
                column: "LocationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreSettings");
        }
    }
}
