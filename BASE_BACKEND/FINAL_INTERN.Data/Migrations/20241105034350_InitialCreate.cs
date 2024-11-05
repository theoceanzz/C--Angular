using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_INTERN.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriesOfCar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__3214EC27DBCA6BBE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NameOfRole = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__3214EC278E0ADF73", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CarInfos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Years = table.Column<DateTime>(type: "date", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Transmission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FuelType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StockQuantit = table.Column<int>(type: "int", nullable: true, defaultValue: 5),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    img = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    alt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    CategoriesOfCar_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarInfos__3214EC27A1D9C638", x => x.ID);
                    table.ForeignKey(
                        name: "FK__CarInfos__Catego__3F466844",
                        column: x => x.CategoriesOfCar_ID,
                        principalTable: "CategoriesOfCar",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Birthday = table.Column<DateTime>(type: "date", nullable: true),
                    img = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    alt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    Role_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Accounts__3214EC2721221F77", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Accounts__Role_I__398D8EEE",
                        column: x => x.Role_ID,
                        principalTable: "Roles",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account_ID = table.Column<int>(type: "int", nullable: true),
                    NameOfCustomer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    date = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__3214EC27A35FB73B", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Orders__Account___4222D4EF",
                        column: x => x.Account_ID,
                        principalTable: "Accounts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_ID = table.Column<int>(type: "int", nullable: true),
                    Account_ID = table.Column<int>(type: "int", nullable: true),
                    Car_ID = table.Column<int>(type: "int", nullable: true),
                    NameOfCar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__3214EC272D446259", x => x.ID);
                    table.ForeignKey(
                        name: "FK__OrderDeta__Accou__45F365D3",
                        column: x => x.Account_ID,
                        principalTable: "Accounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__OrderDeta__Car_I__46E78A0C",
                        column: x => x.Car_ID,
                        principalTable: "CarInfos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__OrderDeta__Order__44FF419A",
                        column: x => x.Order_ID,
                        principalTable: "Orders",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Role_ID",
                table: "Accounts",
                column: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CarInfos_CategoriesOfCar_ID",
                table: "CarInfos",
                column: "CategoriesOfCar_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Account_ID",
                table: "OrderDetails",
                column: "Account_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Car_ID",
                table: "OrderDetails",
                column: "Car_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Order_ID",
                table: "OrderDetails",
                column: "Order_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Account_ID",
                table: "Orders",
                column: "Account_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "CarInfos");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "CategoriesOfCar");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
