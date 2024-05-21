using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP_Portal.Migrations
{
    /// <inheritdoc />
    public partial class FieldAddedInOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f37c966-f93c-426e-a332-e1823b64a61c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85ed9e61-bc26-478c-ba4b-80579de00afa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be1e905e-8f0f-401f-ac03-49f83bf101ab");

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("0c3203be-fd03-4655-94fc-0ca654d7385a"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("16e58c97-49f5-428e-b3fb-44dd8bc86b01"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("e973c35c-3794-460a-ad4c-54b416d4994d"));

            migrationBuilder.AddColumn<bool>(
                name: "IsGivenPriced",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33907cc9-8c73-474c-845b-dc813ca4f44a", "2", "User", "User" },
                    { "3c3614f1-492d-4bd6-b4e4-9bb7418824c6", "3", "Digitizer", "Digitizer" },
                    { "90ced9cd-9695-4691-b5a5-4dd2bfef654f", "4", "VectorArtist", "VectorArtist" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc2c6b63-b489-4271-9b61-9441a85903ca", "AQAAAAIAAYagAAAAEJ49gPLlVQngz9GqlStgMk9wjCqWOFES11Uv3pxl9/IcuHhe1Ojyd4RF9QbbyGJFjw==", "e8de5536-ffba-4115-b393-3d43fdb7d15e" });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("20c98998-f322-4bc4-887f-4930cd3d24af"), "Digitize" },
                    { new Guid("7d781d1b-588f-466a-be92-f2fb9e3fdbd5"), "Vector" },
                    { new Guid("99a54f5c-0c7b-4da3-9a14-d1319792592b"), "Patch" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33907cc9-8c73-474c-845b-dc813ca4f44a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c3614f1-492d-4bd6-b4e4-9bb7418824c6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90ced9cd-9695-4691-b5a5-4dd2bfef654f");

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("20c98998-f322-4bc4-887f-4930cd3d24af"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("7d781d1b-588f-466a-be92-f2fb9e3fdbd5"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("99a54f5c-0c7b-4da3-9a14-d1319792592b"));

            migrationBuilder.DropColumn(
                name: "IsGivenPriced",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6f37c966-f93c-426e-a332-e1823b64a61c", "4", "VectorArtist", "VectorArtist" },
                    { "85ed9e61-bc26-478c-ba4b-80579de00afa", "3", "Digitizer", "Digitizer" },
                    { "be1e905e-8f0f-401f-ac03-49f83bf101ab", "2", "User", "User" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "303905e2-6924-4656-b1fe-2ab4cd882031", "AQAAAAIAAYagAAAAEIgrubbm3T01kB89UV378mJkF2qGM3gbTLfk96D9+GeYUTKWh1wQm6iiGabLXtzd8Q==", "4cf18e80-1509-471f-a334-5ded7063d050" });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0c3203be-fd03-4655-94fc-0ca654d7385a"), "Patch" },
                    { new Guid("16e58c97-49f5-428e-b3fb-44dd8bc86b01"), "Digitize" },
                    { new Guid("e973c35c-3794-460a-ad4c-54b416d4994d"), "Vector" }
                });
        }
    }
}
