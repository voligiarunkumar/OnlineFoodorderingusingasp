using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOrdering.Web.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminsTable",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminsTable", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "CustomersTable",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: false),
                    FistName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    CustomerAddress = table.Column<string>(nullable: false),
                    CustomerContactNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersTable", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "FoodTables",
                columns: table => new
                {
                    FoodName = table.Column<string>(maxLength: 50, nullable: false),
                    FoodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    FoodPrice = table.Column<int>(nullable: false),
                    FoodType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTables", x => x.FoodName);
                });

            migrationBuilder.CreateTable(
                name: "OrdersTable",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    FoodName = table.Column<string>(nullable: false),
                    DateofOrder = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CustomerAddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersTable", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_OrdersTable_FoodTables_FoodName",
                        column: x => x.FoodName,
                        principalTable: "FoodTables",
                        principalColumn: "FoodName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersTable_FoodName",
                table: "OrdersTable",
                column: "FoodName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminsTable");

            migrationBuilder.DropTable(
                name: "CustomersTable");

            migrationBuilder.DropTable(
                name: "OrdersTable");

            migrationBuilder.DropTable(
                name: "FoodTables");
        }
    }
}
