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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyCode", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CurrencyCode",
                columns: new[] { "Id", "Code", "CreatedBy", "DateCreated", "DateModified", "ModifiedBy" },
                values: new object[,]
                {
                    { 1, "AUD", null, new DateTime(2024, 12, 21, 16, 11, 0, 442, DateTimeKind.Local).AddTicks(704), new DateTime(2024, 12, 21, 16, 11, 0, 443, DateTimeKind.Local).AddTicks(4809), null },
                    { 2, "USD", null, new DateTime(2024, 12, 21, 16, 11, 0, 443, DateTimeKind.Local).AddTicks(4975), new DateTime(2024, 12, 21, 16, 11, 0, 443, DateTimeKind.Local).AddTicks(4979), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyCode");
        }
    }
}
