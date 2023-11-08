using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddGenreIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Vinyls",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 11, 7, 16, 16, 28, 159, DateTimeKind.Utc).AddTicks(6340));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "BuyDate",
                value: new DateTime(2023, 11, 7, 16, 16, 28, 159, DateTimeKind.Utc).AddTicks(6350));

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 1,
                column: "GenreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 2,
                column: "GenreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 3,
                column: "GenreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 4,
                column: "GenreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vinyls",
                keyColumn: "VinylId",
                keyValue: 5,
                column: "GenreId",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Vinyls");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 11, 7, 10, 20, 27, 792, DateTimeKind.Utc).AddTicks(8390));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "BuyDate",
                value: new DateTime(2023, 11, 7, 10, 20, 27, 792, DateTimeKind.Utc).AddTicks(8390));
        }
    }
}
