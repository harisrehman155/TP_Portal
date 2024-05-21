using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP_Portal.Migrations
{
    /// <inheritdoc />
    public partial class ModelsCreatedForOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15221709-9543-48eb-ae09-863e68eb5344");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c5cea57-0a71-427d-aaf9-f89de867a6e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be2e426c-58a8-46bd-929a-d9312d46222c");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PoNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fabric = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderMedias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUploadedByCustomer = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderMedias_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderMedias_OrderId",
                table: "OrderMedias",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderMedias");

            migrationBuilder.DropTable(
                name: "Orders");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15221709-9543-48eb-ae09-863e68eb5344", "3", "Digitizer", "Digitizer" },
                    { "5c5cea57-0a71-427d-aaf9-f89de867a6e5", "2", "User", "User" },
                    { "be2e426c-58a8-46bd-929a-d9312d46222c", "4", "VectorArtist", "VectorArtist" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb5430f0-8abc-4805-977e-ad7d2585694f", "AQAAAAIAAYagAAAAEIMN/WBR2qofaC2wR1zfHSHl2JY8t6LeMM2RiTphbXleOYdJi3ZnAXwe65zuqWpVKQ==", "60f70089-7b15-4669-ba20-0622569054ab" });
        }
    }
}
