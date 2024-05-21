using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP_Portal.Migrations
{
    /// <inheritdoc />
    public partial class IsAssignedFiledAddedInOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65e059f4-f2b2-495f-9628-7856bdd1b5b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "723ca0b1-c840-46b9-badd-9d8fe79ca39a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f667f942-f4bd-42f7-967f-fe2933f0489a");

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("41c18dc6-2fef-4628-bf67-fa4f48125b6a"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("78928a34-67f0-402f-b01c-f887dcc666e5"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("d5018a06-2ed7-4c4e-8b0d-d5a7639379d9"));

            migrationBuilder.AddColumn<bool>(
                name: "IsAssigned",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "AssignOrders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "AssignOrders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsAssigned",
                table: "Orders");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "StartTime",
                table: "AssignOrders",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EndTime",
                table: "AssignOrders",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "65e059f4-f2b2-495f-9628-7856bdd1b5b4", "4", "VectorArtist", "VectorArtist" },
                    { "723ca0b1-c840-46b9-badd-9d8fe79ca39a", "3", "Digitizer", "Digitizer" },
                    { "f667f942-f4bd-42f7-967f-fe2933f0489a", "2", "User", "User" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef457463-d674-463a-917b-3610db0476c2", "AQAAAAIAAYagAAAAEFXN0QpWIoVwL+YTMLfsAN+JlKiUyHLrJAXTJdHZ+20xW7J5Nbe0ecbEnbroBp3UlQ==", "00ad9bc9-f547-4b20-908d-e33c41bb1e26" });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("41c18dc6-2fef-4628-bf67-fa4f48125b6a"), "Digitize" },
                    { new Guid("78928a34-67f0-402f-b01c-f887dcc666e5"), "Vector" },
                    { new Guid("d5018a06-2ed7-4c4e-8b0d-d5a7639379d9"), "Patch" }
                });
        }
    }
}
