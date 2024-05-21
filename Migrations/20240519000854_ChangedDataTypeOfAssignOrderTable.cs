using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP_Portal.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDataTypeOfAssignOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignOrders_Pricing_PricingId",
                table: "AssignOrders");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "PricingId",
                table: "AssignOrders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AssignOrders_Pricing_PricingId",
                table: "AssignOrders",
                column: "PricingId",
                principalTable: "Pricing",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignOrders_Pricing_PricingId",
                table: "AssignOrders");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "PricingId",
                table: "AssignOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_AssignOrders_Pricing_PricingId",
                table: "AssignOrders",
                column: "PricingId",
                principalTable: "Pricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
