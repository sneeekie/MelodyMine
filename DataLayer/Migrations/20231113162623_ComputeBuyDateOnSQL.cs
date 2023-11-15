using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ComputeBuyDateOnSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BuyDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 11, 13, 16, 26, 23, 720, DateTimeKind.Utc).AddTicks(7280));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "BuyDate",
                value: new DateTime(2023, 11, 13, 16, 26, 23, 720, DateTimeKind.Utc).AddTicks(7290));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BuyDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 11, 13, 16, 19, 0, 904, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "BuyDate",
                value: new DateTime(2023, 11, 13, 16, 19, 0, 904, DateTimeKind.Utc).AddTicks(9350));
        }
    }
}
