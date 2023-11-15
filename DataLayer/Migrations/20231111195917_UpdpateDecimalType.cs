using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdpateDecimalType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Vinyls",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

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

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 1,
                column: "Price",
                value: 127m);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 2,
                column: "Price",
                value: 187m);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 3,
                column: "Price",
                value: 227m);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 4,
                column: "Price",
                value: 777m);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 5,
                column: "Price",
                value: 127m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Vinyls",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 11, 11, 13, 50, 5, 433, DateTimeKind.Utc).AddTicks(500));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "BuyDate",
                value: new DateTime(2023, 11, 11, 13, 50, 5, 433, DateTimeKind.Utc).AddTicks(500));

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 1,
                column: "Price",
                value: 127.0);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 2,
                column: "Price",
                value: 187.0);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 3,
                column: "Price",
                value: 227.0);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 4,
                column: "Price",
                value: 777.0);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 5,
                column: "Price",
                value: 127.0);
        }
    }
}
