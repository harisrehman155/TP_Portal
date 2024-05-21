using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP_Portal.Migrations
{
    /// <inheritdoc />
    public partial class AddingFieldsToAssignOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7031b451-5172-4257-93c0-cc02bdad01f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73f16601-1514-4b40-a4f5-2304d9e538f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf53657e-29bc-4aa0-a07b-1e1c1fc35f34");

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("a4e4e919-d797-43bc-922d-5408438c716c"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("cbc65758-ed13-4346-a47c-b9b58fa8c6c6"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("f0086239-b2ec-4130-8117-218758f62a33"));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "AssignOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b47839c-be22-45f8-a00c-ed60ec0f2bd8", "3", "Digitizer", "Digitizer" },
                    { "1d499ac2-3042-4755-ae34-375d591aa576", "4", "VectorArtist", "VectorArtist" },
                    { "cbf2024d-b49e-4bde-8f8c-63d9af8c2dae", "2", "User", "User" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "082e27c2-e964-4228-84f6-0bc707c42af8", "AQAAAAIAAYagAAAAEOfkNmVo7mygvYeWsyfn5jsZXxMxjTwTZWA5cXbAK6er/lmFRt79ynMAM9kKj+nnpw==", "2e773976-5f8b-48b8-83d3-0b0337e2be74" });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("264c715d-97ab-4937-a06d-23ef48157ab9"), "Patch" },
                    { new Guid("35698fbe-ac72-4931-a698-3fac33828269"), "Vector" },
                    { new Guid("a206316e-1629-4745-a711-9ba16f68a4f7"), "Digitize" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b47839c-be22-45f8-a00c-ed60ec0f2bd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d499ac2-3042-4755-ae34-375d591aa576");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbf2024d-b49e-4bde-8f8c-63d9af8c2dae");

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("264c715d-97ab-4937-a06d-23ef48157ab9"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("35698fbe-ac72-4931-a698-3fac33828269"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("a206316e-1629-4745-a711-9ba16f68a4f7"));

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "AssignOrders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7031b451-5172-4257-93c0-cc02bdad01f9", "3", "Digitizer", "Digitizer" },
                    { "73f16601-1514-4b40-a4f5-2304d9e538f0", "2", "User", "User" },
                    { "cf53657e-29bc-4aa0-a07b-1e1c1fc35f34", "4", "VectorArtist", "VectorArtist" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a57bb3b6-f385-4c97-a2df-3d4c950dac0b", "AQAAAAIAAYagAAAAEONJzFapVE6E9ucZsUk0xFgD963h/u96t8EQCMI40uYc4MmwIQnK/IqmQE7FpQtq/g==", "25d9c3eb-5714-4062-8a77-72b2bccfe647" });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a4e4e919-d797-43bc-922d-5408438c716c"), "Patch" },
                    { new Guid("cbc65758-ed13-4346-a47c-b9b58fa8c6c6"), "Digitize" },
                    { new Guid("f0086239-b2ec-4130-8117-218758f62a33"), "Vector" }
                });
        }
    }
}
