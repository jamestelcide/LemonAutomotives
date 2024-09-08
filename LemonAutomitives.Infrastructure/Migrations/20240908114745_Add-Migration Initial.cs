using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LemonAutomotives.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ProductManufacturer = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ProductModel = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ProductPurchasePrice = table.Column<double>(type: "float", nullable: true),
                    ProductSalePrice = table.Column<double>(type: "float", nullable: true),
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
                    SalespersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalespersonFirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SalespersonLastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SalespersonAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SalespersonPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalespersonStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalespersonTerminationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salespersons", x => x.SalespersonID);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountPercentage = table.Column<double>(type: "float", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountID);
                    table.ForeignKey(
                        name: "FK_Discounts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalespersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    { new Guid("9ffea449-4c4a-4ee3-9311-5f6fb05e3183"), "3847 Burton Avenue", "Kimberly", "Trombetta", "9015978933", new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b4a7761a-c6f5-435f-bddb-e09550e4f14c"), "4116 Franklin Avenue", "Judith", "Marsh", "3618758716", new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c79e9fb1-4454-427a-950f-7b2811cd5491"), "2269 Rose Street", "Dennis", "Sandoval", "7082569698", new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "DiscountID", "BeginDate", "DiscountPercentage", "EndDate", "ProductID" },
                values: new object[,]
                {
                    { new Guid("83757245-6d97-4147-a208-70a9a9864cf3"), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.20000000000000001, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("de69e429-4d55-4438-981f-5db222251600"), new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.14999999999999999, new DateTime(2023, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("f0cd34f3-7ec2-4903-9001-315f662a498d"), new DateTime(2023, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.29999999999999999, new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "ProductCommission", "ProductManufacturer", "ProductModel", "ProductName", "ProductPurchasePrice", "ProductQty", "ProductSalePrice" },
                values: new object[,]
                {
                    { new Guid("32b4adb0-6c06-47ac-8ee8-bb3e4c14fe36"), 0.050000000000000003, "Jeep", "Grand Cherokee", "Jeep Grand Cherokee", 4000.0, 1, null },
                    { new Guid("b9f2c219-8aff-4694-b405-2e7cde670758"), 0.089999999999999997, "Chrysler", "300", "Chrysler 300", 3000.0, 2, null },
                    { new Guid("fec15166-ff7e-49a6-b404-aecdd6872cda"), 0.20000000000000001, "Fiat", "500", "Fiat 500", 5000.0, 5, null }
                });

            migrationBuilder.InsertData(
                table: "Salespersons",
                columns: new[] { "SalespersonID", "SalespersonAddress", "SalespersonFirstName", "SalespersonLastName", "SalespersonPhone", "SalespersonStartDate", "SalespersonTerminationDate" },
                values: new object[,]
                {
                    { new Guid("0890303a-b802-457a-b56e-aec2bfbead68"), "2949 Juniper Drive", "Dennis", "Luczak", "814-393-4893", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3c8125d8-71d3-455a-99b9-93baf8b3f6dd"), "4291 Harley Vincent Drive", "Debra", "Roger", "203-387-2069", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("496cee1e-59b5-4e2c-a6e9-ebfab5d3dd92"), "2408 Hart Ridge Road", "John", "Stewart", "201-395-1953", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("800c3eb8-a586-48e2-aeba-8fe3d0e20306"), "2840 Gambler Lane", "Hunter", "Lahr", "801-306-2352", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SaleID", "CustomerID", "ProductID", "SalesDate", "SalespersonID" },
                values: new object[,]
                {
                    { new Guid("6655a28d-cd0e-48dd-9eb4-ac3a1bbc9e1b"), new Guid("9ffea449-4c4a-4ee3-9311-5f6fb05e3183"), new Guid("b9f2c219-8aff-4694-b405-2e7cde670758"), new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("496cee1e-59b5-4e2c-a6e9-ebfab5d3dd92") },
                    { new Guid("a6c0de2a-d93a-42f8-a3e9-c90711f98c6c"), new Guid("b4a7761a-c6f5-435f-bddb-e09550e4f14c"), new Guid("fec15166-ff7e-49a6-b404-aecdd6872cda"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("800c3eb8-a586-48e2-aeba-8fe3d0e20306") },
                    { new Guid("efcaa7f0-0c41-44b4-a188-cc20781d23c1"), new Guid("c79e9fb1-4454-427a-950f-7b2811cd5491"), new Guid("32b4adb0-6c06-47ac-8ee8-bb3e4c14fe36"), new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0890303a-b802-457a-b56e-aec2bfbead68") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ProductID",
                table: "Discounts",
                column: "ProductID",
                unique: true,
                filter: "[ProductID] IS NOT NULL");

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
                name: "Discounts");

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
