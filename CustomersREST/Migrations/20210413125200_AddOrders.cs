using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomersREST.Migrations
{
    public partial class AddOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CloseDate", "CustomerId", "Total", "VehicleId" },
                values: new object[] { new Guid("e5125ed8-7a96-4c7d-9399-64c6f604a1d0"), new DateTime(2021, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"), 113.25m, new Guid("4c264d3a-903f-4428-b372-cf7fdfea9855") });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CloseDate", "CustomerId", "Total", "VehicleId" },
                values: new object[] { new Guid("4f3bedd8-2e80-405f-b188-713c11d844c5"), new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"), 99m, new Guid("f841cd18-d50b-444f-96cb-a5e241e42a8a") });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VehicleId",
                table: "Orders",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
