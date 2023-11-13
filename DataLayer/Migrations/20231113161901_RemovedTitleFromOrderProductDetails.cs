using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTitleFromOrderProductDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "OrderProductDetails");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "OrderProductDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "OrderProductDetails",
                keyColumn: "OrderProductDetailsId",
                keyValue: 1,
                column: "Title",
                value: "Dansktop");

            migrationBuilder.UpdateData(
                table: "OrderProductDetails",
                keyColumn: "OrderProductDetailsId",
                keyValue: 2,
                column: "Title",
                value: "Ye");

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
    }
}
