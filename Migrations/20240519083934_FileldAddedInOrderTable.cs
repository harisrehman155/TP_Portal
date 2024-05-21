using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP_Portal.Migrations
{
    /// <inheritdoc />
    public partial class FileldAddedInOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52ce8925-175e-4c71-9602-5b60c4c0ac48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4f08ac8-38a5-4956-98eb-bef2e6f9a2ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f140044b-73d5-42cc-bd82-5397277759b6");

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("1854bea4-64c0-4da3-8547-7c8284223e58"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("3dd7d678-9e14-4fe2-be9e-9e9f5f8779aa"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("a66830da-2d4b-41cc-b0e9-51fcfa1a8de0"));

            migrationBuilder.AddColumn<string>(
                name: "OrderPrice",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a60ac29c-5870-4569-98e3-792d799db926", "4", "VectorArtist", "VectorArtist" },
                    { "c722c530-969e-4c29-a244-0a6988a7dbce", "3", "Digitizer", "Digitizer" },
                    { "fb6637ed-6c85-4e22-8a0e-c62f01785b7e", "2", "User", "User" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01074ef4-0d81-4539-a03f-baf7baceeff8", "AQAAAAIAAYagAAAAEE+MHE7bOcHhkSmqKOjZDvUBhlw4/3W1S9dunZgbykJyI+oQNeKVYP3CgfmH18heTA==", "602d2cb4-fd8d-4d61-87b0-f05f5cfc6501" });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1546b53c-0ea7-4452-bb23-31feac332c42"), "Vector" },
                    { new Guid("866d7cd1-e0c6-43fc-b7ff-8daf3332b055"), "Patch" },
                    { new Guid("9dcd7622-749c-4180-8edb-e6bce061688f"), "Digitize" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "OrderPrice",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52ce8925-175e-4c71-9602-5b60c4c0ac48", "3", "Digitizer", "Digitizer" },
                    { "d4f08ac8-38a5-4956-98eb-bef2e6f9a2ad", "2", "User", "User" },
                    { "f140044b-73d5-42cc-bd82-5397277759b6", "4", "VectorArtist", "VectorArtist" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "494208f6-6901-461e-a584-0a257eb074af", "AQAAAAIAAYagAAAAELQmXyocBpQgFNIxzeECgVrVXevteFdr/k+t0mZGQLNq78+ZXxpXUbsuQZ4NYZsWHA==", "aba3b6ad-3a87-487d-a958-f8551d66ecde" });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1854bea4-64c0-4da3-8547-7c8284223e58"), "Digitize" },
                    { new Guid("3dd7d678-9e14-4fe2-be9e-9e9f5f8779aa"), "Vector" },
                    { new Guid("a66830da-2d4b-41cc-b0e9-51fcfa1a8de0"), "Patch" }
                });
        }
    }
}
