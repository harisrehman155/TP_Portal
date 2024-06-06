using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_Portal.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeInvoiceDone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Invoice",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeePricingId",
                table: "AssignOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaidToEmployee",
                table: "AssignOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_AspNetUsers_CustomerId",
                table: "Invoice",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_AspNetUsers_CustomerId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "EmployeePricingId",
                table: "AssignOrders");

            migrationBuilder.DropColumn(
                name: "IsPaidToEmployee",
                table: "AssignOrders");
        }
    }
}
