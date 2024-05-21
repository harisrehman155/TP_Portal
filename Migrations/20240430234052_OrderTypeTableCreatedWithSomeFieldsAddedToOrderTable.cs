using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP_Portal.Migrations
{
    /// <inheritdoc />
    public partial class OrderTypeTableCreatedWithSomeFieldsAddedToOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23385f7d-5e21-45fa-8e31-2ba0dd50c627");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dbb2f3a-7732-48e4-be6e-ee11d221df51");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba79b1ff-bf93-419c-85a2-da803c3ce420");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderTypeId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9b594e7c-5fce-4c5a-869b-5079c25fc9cb", "3", "Digitizer", "Digitizer" },
                    { "ad687ed6-7e6e-4d49-8ed9-1cc7f23739a1", "2", "User", "User" },
                    { "fc661d00-3056-48f3-9164-3fb4a8216196", "4", "VectorArtist", "VectorArtist" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "625aec6d-50aa-4f2e-9f91-dcfb03aa6a2d", "AQAAAAIAAYagAAAAEA0UEaXhG6g5TRWyLWGC2hHYyum289jr/lp+A/ZPO5W40OWRunGGjtNNBER+TWNYGQ==", "fc6d9a16-f36a-438f-8292-1897f306485f" });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("713570b9-aead-47a1-b538-ab92464d9f7b"), "Digitize" },
                    { new Guid("b98c07b8-c149-4129-8f1b-224ec204f831"), "Vector" },
                    { new Guid("f29be8a5-c5b2-408d-acef-8d55fe0319a7"), "Patch" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderTypes_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId",
                principalTable: "OrderTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderTypes_OrderTypeId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderTypes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b594e7c-5fce-4c5a-869b-5079c25fc9cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad687ed6-7e6e-4d49-8ed9-1cc7f23739a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc661d00-3056-48f3-9164-3fb4a8216196");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23385f7d-5e21-45fa-8e31-2ba0dd50c627", "3", "Digitizer", "Digitizer" },
                    { "5dbb2f3a-7732-48e4-be6e-ee11d221df51", "4", "VectorArtist", "VectorArtist" },
                    { "ba79b1ff-bf93-419c-85a2-da803c3ce420", "2", "User", "User" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00779d24-0fd8-46f2-90cf-a53ccbdf88bd", "AQAAAAIAAYagAAAAEBP2fjcYHLgbtM9kMRD4xrQnmAZAr9GVKd+auK6Ehn3J7/v6uDJ0QUpfDxNWveiU3g==", "d2be35e6-0f38-4294-930f-647809b833ce" });
        }
    }
}
