using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP_Portal.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a60ac29c-5870-4569-98e3-792d799db926");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c722c530-969e-4c29-a244-0a6988a7dbce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb6637ed-6c85-4e22-8a0e-c62f01785b7e");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c7690724-1586-4d79-accb-a5ab13acbef2", "cf5edd27-228f-4db4-bffb-207469ce9893" });

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("1546b53c-0ea7-4452-bb23-31feac332c42"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("866d7cd1-e0c6-43fc-b7ff-8daf3332b055"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("9dcd7622-749c-4180-8edb-e6bce061688f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7690724-1586-4d79-accb-a5ab13acbef2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893");

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_InvoiceId",
                table: "Orders",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Invoice_InvoiceId",
                table: "Orders",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Invoice_InvoiceId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Orders_InvoiceId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a60ac29c-5870-4569-98e3-792d799db926", "4", "VectorArtist", "VectorArtist" },
                    { "c722c530-969e-4c29-a244-0a6988a7dbce", "3", "Digitizer", "Digitizer" },
                    { "c7690724-1586-4d79-accb-a5ab13acbef2", "1", "Admin", "Admin" },
                    { "fb6637ed-6c85-4e22-8a0e-c62f01785b7e", "2", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "Company", "CompanyType", "CompanyWebsiteUrl", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cf5edd27-228f-4db4-bffb-207469ce9893", 0, "123 Main St", "Karachi", "Terminator Punch", "Technology", "https://www.terminatorpunch.com", "01074ef4-0d81-4539-a03f-baf7baceeff8", "harisrehman155@gmail.com", true, true, false, null, "HARISREHMAN155@GMAIL.COM", "HARIS155", "AQAAAAIAAYagAAAAEE+MHE7bOcHhkSmqKOjZDvUBhlw4/3W1S9dunZgbykJyI+oQNeKVYP3CgfmH18heTA==", "03112640322", false, "602d2cb4-fd8d-4d61-87b0-f05f5cfc6501", false, "Haris155" });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1546b53c-0ea7-4452-bb23-31feac332c42"), "Vector" },
                    { new Guid("866d7cd1-e0c6-43fc-b7ff-8daf3332b055"), "Patch" },
                    { new Guid("9dcd7622-749c-4180-8edb-e6bce061688f"), "Digitize" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c7690724-1586-4d79-accb-a5ab13acbef2", "cf5edd27-228f-4db4-bffb-207469ce9893" });
        }
    }
}
