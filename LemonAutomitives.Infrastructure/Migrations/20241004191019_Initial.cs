using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LemonAutomotives.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerFirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerLastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerStartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ProductManufacturer = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ProductModel = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ProductYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductPurchasePrice = table.Column<double>(type: "float", nullable: true),
                    ProductQty = table.Column<int>(type: "int", nullable: false),
                    ProductCommission = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Salespersons",
                columns: table => new
                {
                    SalespersonID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalespersonFirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalespersonLastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalespersonAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SalespersonPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalespersonStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalespersonTerminationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salespersons", x => x.SalespersonID);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceSold = table.Column<double>(type: "float", nullable: false),
                    Commission = table.Column<double>(type: "float", nullable: false),
                    CommissionEarnings = table.Column<double>(type: "float", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalespersonID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleID);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Salespersons_SalespersonID",
                        column: x => x.SalespersonID,
                        principalTable: "Salespersons",
                        principalColumn: "SalespersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "CustomerAddress", "CustomerFirstName", "CustomerLastName", "CustomerPhone", "CustomerStartDate" },
                values: new object[,]
                {
                    { "CU-DENNIS-SANDOVAL-56892", "2269 Rose Street", "Dennis", "Sandoval", "7082569698", new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CU-JUDITH-MARSH-36770", "4116 Franklin Avenue", "Judith", "Marsh", "3618758716", new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CU-KIMBERLY-TROMBETTA-45304", "3847 Burton Avenue", "Kimberly", "Trombetta", "9015978933", new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "ProductCommission", "ProductManufacturer", "ProductModel", "ProductName", "ProductPurchasePrice", "ProductQty", "ProductYear" },
                values: new object[,]
                {
                    { "2007-JEEP-GRAND CHEROKEE", 0.050000000000000003, "Jeep", "Grand Cherokee", "2007 Jeep Grand Cherokee", 4000.0, 1, "2007" },
                    { "2012-FIAT-500", 0.20000000000000001, "Fiat", "500", "2012 Fiat 500", 5000.0, 5, "2012" },
                    { "2015-CHRYSLER-300", 0.089999999999999997, "Chrysler", "300", "2015 Chrysler 300", 3000.0, 2, "2015" }
                });

            migrationBuilder.InsertData(
                table: "Salespersons",
                columns: new[] { "SalespersonID", "SalespersonAddress", "SalespersonFirstName", "SalespersonLastName", "SalespersonPhone", "SalespersonStartDate", "SalespersonTerminationDate" },
                values: new object[,]
                {
                    { "DLUCZAK88957", "2949 Juniper Drive", "Dennis", "Luczak", "8143934893", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "DROGER52760", "4291 Harley Vincent Drive", "Debra", "Roger", "2033872069", new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { "JSTEWART33126", "2408 Hart Ridge Road", "John", "Stewart", "2013951953", new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { "LHUNTER84140", "2840 Gambler Lane", "Hunter", "Lahr", "8013062352", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SaleID", "Commission", "CommissionEarnings", "CustomerID", "PriceSold", "ProductID", "SalesDate", "SalespersonID" },
                values: new object[,]
                {
                    { new Guid("1dda1c36-4e5f-4f7d-9171-aad4d575c2be"), 0.050000000000000003, 270.0, "CU-DENNIS-SANDOVAL-56892", 6000.0, "2015-CHRYSLER-300", new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "DLUCZAK88957" },
                    { new Guid("3a345fcd-d4a2-4d88-a1e3-06b2777bb438"), 0.050000000000000003, 200.0, "CU-KIMBERLY-TROMBETTA-45304", 4000.0, "2007-JEEP-GRAND CHEROKEE", new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "JSTEWART33126" },
                    { new Guid("b36c0a4f-ba43-4d96-90ff-2b6a968c7981"), 0.20000000000000001, 1000.0, "CU-JUDITH-MARSH-36770", 5000.0, "2012-FIAT-500", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LHUNTER84140" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerID",
                table: "Sales",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductID",
                table: "Sales",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SalespersonID",
                table: "Sales",
                column: "SalespersonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Salespersons");
        }
    }
}
