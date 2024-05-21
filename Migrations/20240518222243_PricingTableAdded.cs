using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP_Portal.Migrations
{
    /// <inheritdoc />
    public partial class PricingTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "PricingId",
                table: "AssignOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Pricing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pricing_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pricing_OrderTypes_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderTypes",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AssignOrders_PricingId",
                table: "AssignOrders",
                column: "PricingId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricing_CustomerId",
                table: "Pricing",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricing_OrderTypeId",
                table: "Pricing",
                column: "OrderTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignOrders_Pricing_PricingId",
                table: "AssignOrders",
                column: "PricingId",
                principalTable: "Pricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignOrders_Pricing_PricingId",
                table: "AssignOrders");

            migrationBuilder.DropTable(
                name: "Pricing");

            migrationBuilder.DropIndex(
                name: "IX_AssignOrders_PricingId",
                table: "AssignOrders");

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

            migrationBuilder.DropColumn(
                name: "PricingId",
                table: "AssignOrders");

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
    }
}
