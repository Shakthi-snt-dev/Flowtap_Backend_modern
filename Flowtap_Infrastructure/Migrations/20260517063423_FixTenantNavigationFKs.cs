using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowtap_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTenantNavigationFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tenants_TenantId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Tenants_TenantId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxConfigurations_Tenants_TenantId",
                table: "TaxConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxSlabs_Tenants_TenantId",
                table: "TaxSlabs");

            migrationBuilder.DropIndex(
                name: "IX_TaxSlabs_TenantId",
                table: "TaxSlabs");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TenantId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TaxSlabs");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "TaxConfigurations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "Stores",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tenants_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Tenants_CompanyId",
                table: "Stores",
                column: "CompanyId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Tenants_TenantId",
                table: "Stores",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxConfigurations_Tenants_CompanyId",
                table: "TaxConfigurations",
                column: "CompanyId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxConfigurations_Tenants_TenantId",
                table: "TaxConfigurations",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxSlabs_Tenants_CompanyId",
                table: "TaxSlabs",
                column: "CompanyId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tenants_CompanyId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Tenants_CompanyId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Tenants_TenantId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxConfigurations_Tenants_CompanyId",
                table: "TaxConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxConfigurations_Tenants_TenantId",
                table: "TaxConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxSlabs_Tenants_CompanyId",
                table: "TaxSlabs");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "TaxSlabs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "TaxConfigurations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "Stores",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Employees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaxSlabs_TenantId",
                table: "TaxSlabs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TenantId",
                table: "Employees",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tenants_TenantId",
                table: "Employees",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Tenants_TenantId",
                table: "Stores",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxConfigurations_Tenants_TenantId",
                table: "TaxConfigurations",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxSlabs_Tenants_TenantId",
                table: "TaxSlabs",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
