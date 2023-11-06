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
                name: "Covers",
                columns: table => new
                {
                    VinylCoverId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    VinylId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Covers", x => x.VinylCoverId);
                    table.ForeignKey(
                        name: "FK_Covers_Vinyls_VinylId",
                        column: x => x.VinylId,
                        principalTable: "Vinyls",
                        principalColumn: "VinylId",
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
                name: "vinylGenres",
                columns: table => new
                {
                    VinylId = table.Column<int>(type: "integer", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vinylGenres", x => new { x.VinylId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_vinylGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vinylGenres_Vinyls_VinylId",
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
                    { 1, null, 20.140000000000001, 1, "Album-1" },
                    { 2, null, 31.350000000000001, 1, "Album-2" },
                    { 3, null, 17.960000000000001, 1, "Album-3" },
                    { 4, null, 47.07, 3, "Album-4" },
                    { 5, null, 25.809999999999999, 4, "Album-5" },
                    { 6, null, 21.359999999999999, 1, "Album-6" },
                    { 7, null, 19.57, 3, "Album-7" },
                    { 8, null, 36.450000000000003, 1, "Album-8" },
                    { 9, null, 13.44, 4, "Album-9" },
                    { 10, null, 38.200000000000003, 3, "Album-10" },
                    { 11, null, 49.149999999999999, 3, "Album-11" },
                    { 12, null, 29.969999999999999, 4, "Album-12" },
                    { 13, null, 39.259999999999998, 2, "Album-13" },
                    { 14, null, 21.190000000000001, 3, "Album-14" },
                    { 15, null, 13.75, 2, "Album-15" },
                    { 16, null, 24.920000000000002, 4, "Album-16" },
                    { 17, null, 20.539999999999999, 2, "Album-17" },
                    { 18, null, 41.100000000000001, 3, "Album-18" },
                    { 19, null, 43.600000000000001, 2, "Album-19" },
                    { 20, null, 40.219999999999999, 2, "Album-20" },
                    { 21, null, 19.25, 3, "Album-21" },
                    { 22, null, 25.940000000000001, 4, "Album-22" },
                    { 23, null, 49.490000000000002, 1, "Album-23" },
                    { 24, null, 44.210000000000001, 1, "Album-24" },
                    { 25, null, 39.840000000000003, 1, "Album-25" },
                    { 26, null, 20.449999999999999, 4, "Album-26" },
                    { 27, null, 48.719999999999999, 2, "Album-27" },
                    { 28, null, 49.600000000000001, 4, "Album-28" },
                    { 29, null, 30.280000000000001, 2, "Album-29" },
                    { 30, null, 48.560000000000002, 3, "Album-30" },
                    { 31, null, 15.08, 2, "Album-31" },
                    { 32, null, 16.440000000000001, 1, "Album-32" },
                    { 33, null, 23.059999999999999, 1, "Album-33" },
                    { 34, null, 27.98, 2, "Album-34" },
                    { 35, null, 41.25, 1, "Album-35" },
                    { 36, null, 14.210000000000001, 2, "Album-36" },
                    { 37, null, 25.460000000000001, 1, "Album-37" },
                    { 38, null, 11.130000000000001, 2, "Album-38" },
                    { 39, null, 17.77, 4, "Album-39" },
                    { 40, null, 21.370000000000001, 1, "Album-40" },
                    { 41, null, 27.199999999999999, 2, "Album-41" },
                    { 42, null, 37.07, 2, "Album-42" },
                    { 43, null, 36.289999999999999, 1, "Album-43" },
                    { 44, null, 14.529999999999999, 4, "Album-44" },
                    { 45, null, 14.720000000000001, 3, "Album-45" },
                    { 46, null, 47.719999999999999, 3, "Album-46" },
                    { 47, null, 20.969999999999999, 4, "Album-47" },
                    { 48, null, 25.449999999999999, 3, "Album-48" },
                    { 49, null, 43.439999999999998, 4, "Album-49" },
                    { 50, null, 20.489999999999998, 4, "Album-50" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "NumStars", "ReviewComment", "VinylId" },
                values: new object[,]
                {
                    { 1, 3, "Review for album-1", 1 },
                    { 2, 1, "Review for album-2", 2 },
                    { 3, 4, "Review for album-3", 3 },
                    { 4, 3, "Review for album-4", 4 },
                    { 5, 4, "Review for album-5", 5 },
                    { 6, 2, "Review for album-6", 6 },
                    { 7, 4, "Review for album-7", 7 },
                    { 8, 4, "Review for album-8", 8 },
                    { 9, 4, "Review for album-9", 9 },
                    { 10, 1, "Review for album-10", 10 },
                    { 11, 2, "Review for album-11", 11 },
                    { 12, 3, "Review for album-12", 12 },
                    { 13, 1, "Review for album-13", 13 },
                    { 14, 4, "Review for album-14", 14 },
                    { 15, 4, "Review for album-15", 15 },
                    { 16, 2, "Review for album-16", 16 },
                    { 17, 1, "Review for album-17", 17 },
                    { 18, 4, "Review for album-18", 18 },
                    { 19, 1, "Review for album-19", 19 },
                    { 20, 3, "Review for album-20", 20 },
                    { 21, 3, "Review for album-21", 21 },
                    { 22, 3, "Review for album-22", 22 },
                    { 23, 4, "Review for album-23", 23 },
                    { 24, 2, "Review for album-24", 24 },
                    { 25, 1, "Review for album-25", 25 },
                    { 26, 3, "Review for album-26", 26 },
                    { 27, 2, "Review for album-27", 27 },
                    { 28, 4, "Review for album-28", 28 },
                    { 29, 3, "Review for album-29", 29 },
                    { 30, 3, "Review for album-30", 30 },
                    { 31, 1, "Review for album-31", 31 },
                    { 32, 4, "Review for album-32", 32 },
                    { 33, 3, "Review for album-33", 33 },
                    { 34, 3, "Review for album-34", 34 },
                    { 35, 1, "Review for album-35", 35 },
                    { 36, 4, "Review for album-36", 36 },
                    { 37, 4, "Review for album-37", 37 },
                    { 38, 3, "Review for album-38", 38 },
                    { 39, 3, "Review for album-39", 39 },
                    { 40, 3, "Review for album-40", 40 },
                    { 41, 1, "Review for album-41", 41 },
                    { 42, 4, "Review for album-42", 42 },
                    { 43, 1, "Review for album-43", 43 },
                    { 44, 1, "Review for album-44", 44 },
                    { 45, 4, "Review for album-45", 45 },
                    { 46, 3, "Review for album-46", 46 },
                    { 47, 1, "Review for album-47", 47 },
                    { 48, 2, "Review for album-48", 48 },
                    { 49, 3, "Review for album-49", 49 },
                    { 50, 3, "Review for album-50", 50 }
                });

            migrationBuilder.InsertData(
                table: "vinylGenres",
                columns: new[] { "GenreId", "VinylId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 6, 2 },
                    { 7, 3 },
                    { 3, 4 },
                    { 9, 5 },
                    { 7, 6 },
                    { 8, 7 },
                    { 2, 8 },
                    { 5, 9 },
                    { 4, 10 },
                    { 3, 11 },
                    { 9, 12 },
                    { 2, 13 },
                    { 7, 14 },
                    { 2, 15 },
                    { 3, 16 },
                    { 4, 17 },
                    { 5, 18 },
                    { 5, 19 },
                    { 1, 20 },
                    { 5, 21 },
                    { 4, 22 },
                    { 7, 23 },
                    { 2, 24 },
                    { 2, 25 },
                    { 1, 26 },
                    { 5, 27 },
                    { 9, 28 },
                    { 1, 29 },
                    { 6, 30 },
                    { 9, 31 },
                    { 9, 32 },
                    { 7, 33 },
                    { 1, 34 },
                    { 2, 35 },
                    { 4, 36 },
                    { 8, 37 },
                    { 6, 38 },
                    { 6, 39 },
                    { 1, 40 },
                    { 7, 41 },
                    { 8, 42 },
                    { 7, 43 },
                    { 5, 44 },
                    { 3, 45 },
                    { 1, 46 },
                    { 7, 47 },
                    { 9, 48 },
                    { 7, 49 },
                    { 3, 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Covers_VinylId",
                table: "Covers",
                column: "VinylId");

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
                name: "IX_vinylGenres_GenreId",
                table: "vinylGenres",
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
                name: "Covers");

            migrationBuilder.DropTable(
                name: "OrderProductDetails");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "vinylGenres");

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
