using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExchangeRate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 3, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QueryHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InputCurrency = table.Column<string>(type: "TEXT", nullable: false),
                    OutputCurrancy = table.Column<string>(type: "TEXT", nullable: false),
                    Rate = table.Column<double>(type: "REAL", nullable: false),
                    DateQueried = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryHistory", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CurrencyCode",
                columns: new[] { "Id", "Code", "CreatedBy", "DateCreated", "DateModified", "ModifiedBy" },
                values: new object[,]
                {
                    { 1, "AUD", null, new DateTime(2024, 12, 22, 16, 3, 23, 514, DateTimeKind.Local).AddTicks(6237), new DateTime(2024, 12, 22, 16, 3, 23, 516, DateTimeKind.Local).AddTicks(7336), null },
                    { 2, "USD", null, new DateTime(2024, 12, 22, 16, 3, 23, 516, DateTimeKind.Local).AddTicks(7549), new DateTime(2024, 12, 22, 16, 3, 23, 516, DateTimeKind.Local).AddTicks(7554), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyCode");

            migrationBuilder.DropTable(
                name: "QueryHistory");
        }
    }
}
