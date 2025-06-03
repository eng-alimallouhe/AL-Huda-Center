using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatethelinkbetweenorderstables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrintOrders_Orders_OrderId",
                table: "PrintOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_PrintOrders_BaseOrders_OrderId",
                table: "PrintOrders",
                column: "OrderId",
                principalTable: "BaseOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrintOrders_BaseOrders_OrderId",
                table: "PrintOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_PrintOrders_Orders_OrderId",
                table: "PrintOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
