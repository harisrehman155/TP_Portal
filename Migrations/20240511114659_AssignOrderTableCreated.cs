using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP_Portal.Migrations
{
    /// <inheritdoc />
    public partial class AssignOrderTableCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dfebb56-134c-4420-a9a4-ecd2a4163396");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c2a650e-6147-4604-850c-66a694f0e2cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "872e16e6-b4fa-42fa-b269-46254213ad40");

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("2fcc935f-61d5-472c-9267-2e519d26c9f3"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("78ced731-c7a9-4cae-be41-51d169ecc596"));

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: new Guid("e3566e8f-9ccc-4a8e-b385-31e0a874bc6b"));

            migrationBuilder.CreateTable(
                name: "AssignOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignOrders_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignOrders_Orders_OrderId",
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

            migrationBuilder.CreateIndex(
                name: "IX_AssignOrders_EmployeeId",
                table: "AssignOrders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignOrders_OrderId",
                table: "AssignOrders",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignOrders");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0dfebb56-134c-4420-a9a4-ecd2a4163396", "2", "User", "User" },
                    { "7c2a650e-6147-4604-850c-66a694f0e2cd", "3", "Digitizer", "Digitizer" },
                    { "872e16e6-b4fa-42fa-b269-46254213ad40", "4", "VectorArtist", "VectorArtist" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf5edd27-228f-4db4-bffb-207469ce9893",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae6a3d82-34de-404e-a325-0987c164824a", "AQAAAAIAAYagAAAAEAqtA3afgSpzoDhs9e5FISBQggTCgz6r+06E8WLEUMnzcZIoGuNzfatlKAEFJ+gGxw==", "d6003b95-f5b4-4593-b7db-ca62480eaf0f" });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2fcc935f-61d5-472c-9267-2e519d26c9f3"), "Vector" },
                    { new Guid("78ced731-c7a9-4cae-be41-51d169ecc596"), "Patch" },
                    { new Guid("e3566e8f-9ccc-4a8e-b385-31e0a874bc6b"), "Digitize" }
                });
        }
    }
}
