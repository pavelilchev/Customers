using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomersREST.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { new Guid("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"), "john.doe@gmail.com", "John", "Doe" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { new Guid("160a78a4-7076-4deb-a369-8a02c25abd23"), null, "Paul", "Curtny" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { new Guid("e0afc479-4259-4f54-b7f3-64bc9460b6b5"), "sp@taipan.ch", "Suji", "Paji" });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CustomerId", "Make", "Mileage" },
                values: new object[] { new Guid("f841cd18-d50b-444f-96cb-a5e241e42a8a"), new Guid("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"), "Lada", 340000 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CustomerId", "Make", "Mileage" },
                values: new object[] { new Guid("4c264d3a-903f-4428-b372-cf7fdfea9855"), new Guid("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"), "Opel", 113000 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CustomerId", "Make", "Mileage" },
                values: new object[] { new Guid("25400098-4516-489c-99b5-9e78a36034a2"), new Guid("e0afc479-4259-4f54-b7f3-64bc9460b6b5"), "Mazda", 22000 });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CustomerId",
                table: "Vehicles",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
