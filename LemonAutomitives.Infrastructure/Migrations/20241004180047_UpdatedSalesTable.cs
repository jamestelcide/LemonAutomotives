using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace LemonAutomotives.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSalesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Commission",
                table: "Sales",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CommissionEarnings",
                table: "Sales",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceSold",
                table: "Sales",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleID",
                keyValue: new Guid("1dda1c36-4e5f-4f7d-9171-aad4d575c2be"),
                columns: new[] { "Commission", "CommissionEarnings", "PriceSold" },
                values: new object[] { 0.050000000000000003, 300.0, 6000.0 });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleID",
                keyValue: new Guid("3a345fcd-d4a2-4d88-a1e3-06b2777bb438"),
                columns: new[] { "Commission", "CommissionEarnings", "PriceSold" },
                values: new object[] { 0.10000000000000001, 500.0, 5000.0 });

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleID",
                keyValue: new Guid("b36c0a4f-ba43-4d96-90ff-2b6a968c7981"),
                columns: new[] { "Commission", "CommissionEarnings", "PriceSold" },
                values: new object[] { 0.029999999999999999, 90.0, 3000.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commission",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CommissionEarnings",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "PriceSold",
                table: "Sales");
        }
    }
}
