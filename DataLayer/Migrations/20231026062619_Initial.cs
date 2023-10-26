using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Postal = table.Column<int>(type: "integer", nullable: false),
                    StreetNumber = table.Column<int>(type: "integer", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    SignedIn = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GenreName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    BuyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "RecordLabels",
                columns: table => new
                {
                    RecordLabelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LabelName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<int>(type: "integer", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    AddressId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordLabels", x => x.RecordLabelId);
                    table.ForeignKey(
                        name: "FK_RecordLabels_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateTable(
                name: "OrderProductDetails",
                columns: table => new
                {
                    OrderProductDetailsId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductDetails", x => x.OrderProductDetailsId);
                    table.ForeignKey(
                        name: "FK_OrderProductDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vinyls",
                columns: table => new
                {
                    VinylId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    RecordLabelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vinyls", x => x.VinylId);
                    table.ForeignKey(
                        name: "FK_Vinyls_RecordLabels_RecordLabelId",
                        column: x => x.RecordLabelId,
                        principalTable: "RecordLabels",
                        principalColumn: "RecordLabelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReviewComment = table.Column<string>(type: "text", nullable: true),
                    NumStars = table.Column<int>(type: "integer", nullable: false),
                    VinylId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Vinyls_VinylId",
                        column: x => x.VinylId,
                        principalTable: "Vinyls",
                        principalColumn: "VinylId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VinylCovers",
                columns: table => new
                {
                    VinylCoverId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    VinylId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VinylCovers", x => x.VinylCoverId);
                    table.ForeignKey(
                        name: "FK_VinylCovers_Vinyls_VinylId",
                        column: x => x.VinylId,
                        principalTable: "Vinyls",
                        principalColumn: "VinylId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VinylGenres",
                columns: table => new
                {
                    VinylId = table.Column<int>(type: "integer", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VinylGenres", x => new { x.VinylId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_VinylGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VinylGenres_Vinyls_VinylId",
                        column: x => x.VinylId,
                        principalTable: "Vinyls",
                        principalColumn: "VinylId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "Password", "SignedIn", "Username" },
                values: new object[] { 1, "123", false, "Administrator" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[,]
                {
                    { 1, "Rock" },
                    { 2, "Pop" },
                    { 3, "Jazz" },
                    { 4, "Classical" },
                    { 5, "Blues" },
                    { 6, "Country" },
                    { 7, "Reggae" },
                    { 8, "Hip-Hop" },
                    { 9, "Electronic" },
                    { 10, "Folk" }
                });

            migrationBuilder.InsertData(
                table: "RecordLabels",
                columns: new[] { "RecordLabelId", "AddressId", "Email", "LabelName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, null, null, "Label-1", null },
                    { 2, null, null, "Label-2", null },
                    { 3, null, null, "Label-3", null },
                    { 4, null, null, "Label-4", null },
                    { 5, null, null, "Label-5", null }
                });

            migrationBuilder.InsertData(
                table: "Vinyls",
                columns: new[] { "VinylId", "Description", "Price", "RecordLabelId", "Title" },
                values: new object[,]
                {
                    { 1, null, 25.690000000000001, 1, "Album-1" },
                    { 2, null, 25.5, 3, "Album-2" },
                    { 3, null, 13.75, 4, "Album-3" },
                    { 4, null, 10.5, 1, "Album-4" },
                    { 5, null, 26.140000000000001, 3, "Album-5" },
                    { 6, null, 16.670000000000002, 2, "Album-6" },
                    { 7, null, 16.780000000000001, 4, "Album-7" },
                    { 8, null, 29.300000000000001, 3, "Album-8" },
                    { 9, null, 18.309999999999999, 3, "Album-9" },
                    { 10, null, 17.870000000000001, 4, "Album-10" },
                    { 11, null, 25.48, 2, "Album-11" },
                    { 12, null, 31.850000000000001, 1, "Album-12" },
                    { 13, null, 28.600000000000001, 2, "Album-13" },
                    { 14, null, 44.890000000000001, 3, "Album-14" },
                    { 15, null, 28.23, 3, "Album-15" },
                    { 16, null, 44.170000000000002, 3, "Album-16" },
                    { 17, null, 26.649999999999999, 4, "Album-17" },
                    { 18, null, 31.620000000000001, 3, "Album-18" },
                    { 19, null, 41.899999999999999, 4, "Album-19" },
                    { 20, null, 41.259999999999998, 2, "Album-20" },
                    { 21, null, 16.59, 4, "Album-21" },
                    { 22, null, 11.99, 2, "Album-22" },
                    { 23, null, 46.060000000000002, 1, "Album-23" },
                    { 24, null, 21.079999999999998, 2, "Album-24" },
                    { 25, null, 20.050000000000001, 4, "Album-25" },
                    { 26, null, 43.979999999999997, 2, "Album-26" },
                    { 27, null, 34.289999999999999, 3, "Album-27" },
                    { 28, null, 18.739999999999998, 3, "Album-28" },
                    { 29, null, 35.829999999999998, 3, "Album-29" },
                    { 30, null, 47.280000000000001, 2, "Album-30" },
                    { 31, null, 31.190000000000001, 1, "Album-31" },
                    { 32, null, 41.350000000000001, 3, "Album-32" },
                    { 33, null, 19.140000000000001, 1, "Album-33" },
                    { 34, null, 48.93, 2, "Album-34" },
                    { 35, null, 40.090000000000003, 1, "Album-35" },
                    { 36, null, 14.970000000000001, 4, "Album-36" },
                    { 37, null, 39.780000000000001, 3, "Album-37" },
                    { 38, null, 35.020000000000003, 3, "Album-38" },
                    { 39, null, 36.159999999999997, 3, "Album-39" },
                    { 40, null, 18.350000000000001, 4, "Album-40" },
                    { 41, null, 17.98, 3, "Album-41" },
                    { 42, null, 39.829999999999998, 2, "Album-42" },
                    { 43, null, 40.009999999999998, 4, "Album-43" },
                    { 44, null, 39.530000000000001, 1, "Album-44" },
                    { 45, null, 25.739999999999998, 1, "Album-45" },
                    { 46, null, 17.059999999999999, 3, "Album-46" },
                    { 47, null, 12.27, 4, "Album-47" },
                    { 48, null, 33.740000000000002, 2, "Album-48" },
                    { 49, null, 16.690000000000001, 3, "Album-49" },
                    { 50, null, 10.18, 2, "Album-50" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "NumStars", "ReviewComment", "VinylId" },
                values: new object[,]
                {
                    { 1, 3, "Review for album-1", 1 },
                    { 2, 4, "Review for album-2", 2 },
                    { 3, 2, "Review for album-3", 3 },
                    { 4, 1, "Review for album-4", 4 },
                    { 5, 4, "Review for album-5", 5 },
                    { 6, 2, "Review for album-6", 6 },
                    { 7, 2, "Review for album-7", 7 },
                    { 8, 4, "Review for album-8", 8 },
                    { 9, 1, "Review for album-9", 9 },
                    { 10, 3, "Review for album-10", 10 },
                    { 11, 1, "Review for album-11", 11 },
                    { 12, 3, "Review for album-12", 12 },
                    { 13, 3, "Review for album-13", 13 },
                    { 14, 4, "Review for album-14", 14 },
                    { 15, 1, "Review for album-15", 15 },
                    { 16, 3, "Review for album-16", 16 },
                    { 17, 4, "Review for album-17", 17 },
                    { 18, 2, "Review for album-18", 18 },
                    { 19, 3, "Review for album-19", 19 },
                    { 20, 2, "Review for album-20", 20 },
                    { 21, 3, "Review for album-21", 21 },
                    { 22, 4, "Review for album-22", 22 },
                    { 23, 4, "Review for album-23", 23 },
                    { 24, 3, "Review for album-24", 24 },
                    { 25, 1, "Review for album-25", 25 },
                    { 26, 3, "Review for album-26", 26 },
                    { 27, 1, "Review for album-27", 27 },
                    { 28, 4, "Review for album-28", 28 },
                    { 29, 4, "Review for album-29", 29 },
                    { 30, 1, "Review for album-30", 30 },
                    { 31, 4, "Review for album-31", 31 },
                    { 32, 1, "Review for album-32", 32 },
                    { 33, 3, "Review for album-33", 33 },
                    { 34, 2, "Review for album-34", 34 },
                    { 35, 1, "Review for album-35", 35 },
                    { 36, 4, "Review for album-36", 36 },
                    { 37, 4, "Review for album-37", 37 },
                    { 38, 1, "Review for album-38", 38 },
                    { 39, 3, "Review for album-39", 39 },
                    { 40, 2, "Review for album-40", 40 },
                    { 41, 3, "Review for album-41", 41 },
                    { 42, 4, "Review for album-42", 42 },
                    { 43, 3, "Review for album-43", 43 },
                    { 44, 4, "Review for album-44", 44 },
                    { 45, 4, "Review for album-45", 45 },
                    { 46, 3, "Review for album-46", 46 },
                    { 47, 2, "Review for album-47", 47 },
                    { 48, 1, "Review for album-48", 48 },
                    { 49, 3, "Review for album-49", 49 },
                    { 50, 1, "Review for album-50", 50 }
                });

            migrationBuilder.InsertData(
                table: "VinylGenres",
                columns: new[] { "GenreId", "VinylId" },
                values: new object[,]
                {
                    { 7, 1 },
                    { 8, 2 },
                    { 4, 3 },
                    { 7, 4 },
                    { 2, 5 },
                    { 9, 6 },
                    { 4, 7 },
                    { 9, 8 },
                    { 2, 9 },
                    { 6, 10 },
                    { 7, 11 },
                    { 3, 12 },
                    { 1, 13 },
                    { 7, 14 },
                    { 4, 15 },
                    { 9, 16 },
                    { 3, 17 },
                    { 1, 18 },
                    { 1, 19 },
                    { 5, 20 },
                    { 5, 21 },
                    { 1, 22 },
                    { 6, 23 },
                    { 8, 24 },
                    { 8, 25 },
                    { 2, 26 },
                    { 3, 27 },
                    { 8, 28 },
                    { 6, 29 },
                    { 1, 30 },
                    { 5, 31 },
                    { 3, 32 },
                    { 2, 33 },
                    { 2, 34 },
                    { 5, 35 },
                    { 8, 36 },
                    { 1, 37 },
                    { 4, 38 },
                    { 9, 39 },
                    { 4, 40 },
                    { 5, 41 },
                    { 2, 42 },
                    { 5, 43 },
                    { 5, 44 },
                    { 1, 45 },
                    { 7, 46 },
                    { 6, 47 },
                    { 6, 48 },
                    { 5, 49 },
                    { 2, 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductDetails_OrderId",
                table: "OrderProductDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordLabels_AddressId",
                table: "RecordLabels",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_VinylId",
                table: "Reviews",
                column: "VinylId");

            migrationBuilder.CreateIndex(
                name: "IX_VinylCovers_VinylId",
                table: "VinylCovers",
                column: "VinylId");

            migrationBuilder.CreateIndex(
                name: "IX_VinylGenres_GenreId",
                table: "VinylGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Vinyls_RecordLabelId",
                table: "Vinyls",
                column: "RecordLabelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "OrderProductDetails");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "VinylCovers");

            migrationBuilder.DropTable(
                name: "VinylGenres");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Vinyls");

            migrationBuilder.DropTable(
                name: "RecordLabels");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
