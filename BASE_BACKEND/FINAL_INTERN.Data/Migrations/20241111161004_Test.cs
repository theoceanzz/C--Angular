using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_INTERN.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriesOfCar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__3214EC27B4BDA787", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NameOfRole = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__3214EC27D53A553F", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CarInfos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Years = table.Column<DateTime>(type: "date", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Transmission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FuelType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    StockQuantit = table.Column<int>(type: "int", nullable: true, defaultValue: 5),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    img = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    alt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    CategoriesOfCar_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarInfos__3214EC278791B85C", x => x.ID);
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
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Birthday = table.Column<DateTime>(type: "date", nullable: true),
                    img = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    alt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    Role_ID = table.Column<int>(type: "int", nullable: false),
                    resetPasswordToken = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    resetTokenExpiry = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Accounts__3214EC270215FF01", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Accounts__resetT__398D8EEE",
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
                    Account_ID = table.Column<int>(type: "int", nullable: false),
                    NameOfCustomer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    dates = table.Column<DateTime>(type: "datetime", nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__3214EC273B98EC5C", x => x.ID);
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
                    Order_ID = table.Column<int>(type: "int", nullable: false),
                    Account_ID = table.Column<int>(type: "int", nullable: false),
                    Car_ID = table.Column<int>(type: "int", nullable: false),
                    NameOfCar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    price = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__3214EC275E303269", x => x.ID);
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

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Car_ID = table.Column<int>(type: "int", nullable: true),
                    Account_ID = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<int>(type: "int", nullable: true),
                    Order_ID = table.Column<int>(type: "int", nullable: true),
                    vnp_BankTranNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    vnp_CartType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    vnp_BankCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    vnp_Amount = table.Column<int>(type: "int", nullable: true),
                    vnp_TxnRef = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    vnp_OrderInfo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    vnp_ResponseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    vnp_TransactionNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    vnp_TransactionStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    vnp_PayDate = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    vnp_TmnCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    payment_created_at = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    payments_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__3214EC27AA80C635", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Payments__Accoun__4AB81AF0",
                        column: x => x.Account_ID,
                        principalTable: "Accounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Payments__Car_ID__49C3F6B7",
                        column: x => x.Car_ID,
                        principalTable: "CarInfos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Payments__Order___4BAC3F29",
                        column: x => x.Order_ID,
                        principalTable: "Orders",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    day_of_transaction = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Account_ID = table.Column<int>(type: "int", nullable: true),
                    Order_ID = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<double>(type: "float", nullable: true),
                    total = table.Column<double>(type: "float", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__3214EC27CAF24BFF", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Transacti__Accou__4E88ABD4",
                        column: x => x.Account_ID,
                        principalTable: "Accounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Transacti__Order__4F7CD00D",
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

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Account_ID",
                table: "Payments",
                column: "Account_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Car_ID",
                table: "Payments",
                column: "Car_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Order_ID",
                table: "Payments",
                column: "Order_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Account_ID",
                table: "Transactions",
                column: "Account_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Order_ID",
                table: "Transactions",
                column: "Order_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Transactions");

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
