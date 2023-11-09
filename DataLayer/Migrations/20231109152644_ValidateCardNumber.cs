using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ValidateCardNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderProductDetails",
                keyColumn: "OrderProductDetailsId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Vinyls",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Addresses",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 1,
                column: "CardNumber",
                value: "4111 1111 1111 1111");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 2,
                column: "CardNumber",
                value: "4111 1111 1111 1112");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 11, 9, 15, 26, 44, 106, DateTimeKind.Utc).AddTicks(5140));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "BuyDate",
                value: new DateTime(2023, 11, 9, 15, 26, 44, 106, DateTimeKind.Utc).AddTicks(5150));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Vinyls",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Orders",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CardNumber",
                table: "Addresses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 1,
                column: "CardNumber",
                value: 1244444444444444L);

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 2,
                column: "CardNumber",
                value: 1331131331131331L);

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "CardNumber", "City", "Country", "Postal", "Street", "StreetNumber" },
                values: new object[] { 3, 1331751331131331L, "Fredericia", "Denmark", 7000, "Kongensgade", 2 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 11, 9, 8, 16, 29, 116, DateTimeKind.Utc).AddTicks(4130));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "BuyDate",
                value: new DateTime(2023, 11, 9, 8, 16, 29, 116, DateTimeKind.Utc).AddTicks(4130));

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "AddressId", "BuyDate", "Email" },
                values: new object[] { 3, 3, new DateTime(2023, 11, 9, 8, 16, 29, 116, DateTimeKind.Utc).AddTicks(4140), "nikolaj@example.com" });

            migrationBuilder.InsertData(
                table: "OrderProductDetails",
                columns: new[] { "OrderProductDetailsId", "OrderId", "Price", "Title", "VinylId" },
                values: new object[] { 3, 3, 227.0, "OK Computer", 3 });
        }
    }
}
