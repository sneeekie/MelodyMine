using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class QuantityColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderProductDetails",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderProductDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "OrderProductDetails",
                keyColumn: "OrderProductDetailsId",
                keyValue: 1,
                columns: new[] { "Price", "Quantity" },
                values: new object[] { 127m, 0 });

            migrationBuilder.UpdateData(
                table: "OrderProductDetails",
                keyColumn: "OrderProductDetailsId",
                keyValue: 2,
                columns: new[] { "Price", "Quantity" },
                values: new object[] { 187m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 11, 13, 8, 11, 40, 800, DateTimeKind.Utc).AddTicks(5500));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "BuyDate",
                value: new DateTime(2023, 11, 13, 8, 11, 40, 800, DateTimeKind.Utc).AddTicks(5500));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderProductDetails");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "OrderProductDetails",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "OrderProductDetails",
                keyColumn: "OrderProductDetailsId",
                keyValue: 1,
                column: "Price",
                value: 127.0);

            migrationBuilder.UpdateData(
                table: "OrderProductDetails",
                keyColumn: "OrderProductDetailsId",
                keyValue: 2,
                column: "Price",
                value: 187.0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 11, 11, 19, 59, 17, 309, DateTimeKind.Utc).AddTicks(7000));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "BuyDate",
                value: new DateTime(2023, 11, 11, 19, 59, 17, 309, DateTimeKind.Utc).AddTicks(7010));
        }
    }
}
