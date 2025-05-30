using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class linkEmployeesTableAndPaymentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_EmployeeId",
                table: "Payments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Employees_EmployeeId",
                table: "Payments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Employees_EmployeeId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_EmployeeId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Payments");
        }
    }
}
