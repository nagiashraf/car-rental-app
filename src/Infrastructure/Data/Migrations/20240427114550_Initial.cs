﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrenciesBases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrenciesBases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NativeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FlagImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsRtl = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationsBases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationsBases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrenciesTranslations",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrenciesTranslations", x => new { x.CurrencyId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_CurrenciesTranslations_CurrenciesBases_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrenciesBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrenciesTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllowedLocations",
                columns: table => new
                {
                    PickupLocationId = table.Column<int>(type: "int", nullable: false),
                    DropOffLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedLocations", x => new { x.PickupLocationId, x.DropOffLocationId });
                    table.ForeignKey(
                        name: "FK_AllowedLocations_LocationsBases_DropOffLocationId",
                        column: x => x.DropOffLocationId,
                        principalTable: "LocationsBases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllowedLocations_LocationsBases_PickupLocationId",
                        column: x => x.PickupLocationId,
                        principalTable: "LocationsBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationsTranslations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationsTranslations", x => new { x.LocationId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_LocationsTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationsTranslations_LocationsBases_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LocationsBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllowedLocations_DropOffLocationId",
                table: "AllowedLocations",
                column: "DropOffLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrenciesTranslations_LanguageId",
                table: "CurrenciesTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Code",
                table: "Languages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationsBases_PublicId",
                table: "LocationsBases",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationsTranslations_Country_City_Name",
                table: "LocationsTranslations",
                columns: new[] { "Country", "City", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_LocationsTranslations_LanguageId",
                table: "LocationsTranslations",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowedLocations");

            migrationBuilder.DropTable(
                name: "CurrenciesTranslations");

            migrationBuilder.DropTable(
                name: "LocationsTranslations");

            migrationBuilder.DropTable(
                name: "CurrenciesBases");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "LocationsBases");
        }
    }
}