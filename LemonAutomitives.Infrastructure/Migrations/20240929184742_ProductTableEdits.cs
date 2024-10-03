using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LemonAutomotives.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductTableEdits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropColumn(
                name: "ProductSalePrice",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "SalespersonPhone",
                table: "Salespersons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SalespersonLastName",
                table: "Salespersons",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SalespersonFirstName",
                table: "Salespersons",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SalespersonAddress",
                table: "Salespersons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductYear",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("32b4adb0-6c06-47ac-8ee8-bb3e4c14fe36"),
                columns: new[] { "ProductName", "ProductYear" },
                values: new object[] { "2007 Jeep Grand Cherokee", "2007" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("b9f2c219-8aff-4694-b405-2e7cde670758"),
                columns: new[] { "ProductName", "ProductYear" },
                values: new object[] { "2015 Chrysler 300", "2015" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("fec15166-ff7e-49a6-b404-aecdd6872cda"),
                columns: new[] { "ProductName", "ProductYear" },
                values: new object[] { "2012 Fiat 500", "2012" });

            migrationBuilder.UpdateData(
                table: "Salespersons",
                keyColumn: "SalespersonID",
                keyValue: new Guid("0890303a-b802-457a-b56e-aec2bfbead68"),
                column: "SalespersonStartDate",
                value: new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Salespersons",
                keyColumn: "SalespersonID",
                keyValue: new Guid("3c8125d8-71d3-455a-99b9-93baf8b3f6dd"),
                column: "SalespersonStartDate",
                value: new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Salespersons",
                keyColumn: "SalespersonID",
                keyValue: new Guid("496cee1e-59b5-4e2c-a6e9-ebfab5d3dd92"),
                column: "SalespersonStartDate",
                value: new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductYear",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "SalespersonPhone",
                table: "Salespersons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SalespersonLastName",
                table: "Salespersons",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "SalespersonFirstName",
                table: "Salespersons",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "SalespersonAddress",
                table: "Salespersons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<double>(
                name: "ProductSalePrice",
                table: "Products",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountPercentage = table.Column<double>(type: "float", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "DiscountID", "BeginDate", "DiscountPercentage", "EndDate", "ProductID" },
                values: new object[,]
                {
                    { new Guid("83757245-6d97-4147-a208-70a9a9864cf3"), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.20000000000000001, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("de69e429-4d55-4438-981f-5db222251600"), new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.14999999999999999, new DateTime(2023, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("f0cd34f3-7ec2-4903-9001-315f662a498d"), new DateTime(2023, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.29999999999999999, new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("32b4adb0-6c06-47ac-8ee8-bb3e4c14fe36"),
                columns: new[] { "ProductName", "ProductSalePrice" },
                values: new object[] { "Jeep Grand Cherokee", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("b9f2c219-8aff-4694-b405-2e7cde670758"),
                columns: new[] { "ProductName", "ProductSalePrice" },
                values: new object[] { "Chrysler 300", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("fec15166-ff7e-49a6-b404-aecdd6872cda"),
                columns: new[] { "ProductName", "ProductSalePrice" },
                values: new object[] { "Fiat 500", null });

            migrationBuilder.UpdateData(
                table: "Salespersons",
                keyColumn: "SalespersonID",
                keyValue: new Guid("0890303a-b802-457a-b56e-aec2bfbead68"),
                column: "SalespersonStartDate",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Salespersons",
                keyColumn: "SalespersonID",
                keyValue: new Guid("3c8125d8-71d3-455a-99b9-93baf8b3f6dd"),
                column: "SalespersonStartDate",
                value: new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Salespersons",
                keyColumn: "SalespersonID",
                keyValue: new Guid("496cee1e-59b5-4e2c-a6e9-ebfab5d3dd92"),
                column: "SalespersonStartDate",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ProductID",
                table: "Discounts",
                column: "ProductID",
                unique: true,
                filter: "[ProductID] IS NOT NULL");
        }
    }
}
