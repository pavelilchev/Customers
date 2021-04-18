using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomersREST.Migrations
{
    public partial class AddLocationid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("160a78a4-7076-4deb-a369-8a02c25abd23"),
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"),
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("e0afc479-4259-4f54-b7f3-64bc9460b6b5"),
                column: "LocationId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Customers");
        }
    }
}
