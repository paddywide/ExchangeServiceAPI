using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeRate.Identity.Migrations
{
    /// <inheritdoc />
    public partial class changeRoleNameVisitor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Visitor", "VISITOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67ab3f75-4623-4c52-8291-344731662057", "AQAAAAIAAYagAAAAEApQlPo9LvnUx9ebmilaZ28p5PFlQ0ZDQqc8ZwC9u4311OlWWXuwzsIxUVHuAPy63A==", "f9e458c1-bc80-492d-8990-65948d96758c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d045c659-a73c-471d-be14-0fc2f81a245e", "AQAAAAIAAYagAAAAEHuIxscJDsRI3TxTFHLqCEo3CsuljYF7f4NgkoWOxke+bmcFZe+EHraXA35LnXO+tw==", "0d24c32e-c71d-4ef6-85bc-6f9d3c90c1bf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Vistor", "VISTOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "743bdea3-a5c6-4955-8f6c-a336e6f07be0", "AQAAAAIAAYagAAAAEAhvCrpdIYdv2zo+5RqFATjTAVtPLcXTO8KTMEUvOIXM6KogNVuxsvnxW0VjoO+Z6w==", "25b26624-a5c4-44ce-a691-ac2fafa22705" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cf9b4ff-6a21-45f6-8d27-cb2937d5d44b", "AQAAAAIAAYagAAAAEBuJktN8YO5kHUijOFqyJTZ7apJXeQWNLQ3kgXutIfL9BYQGhOpv9bATskNgNf5QiA==", "6f93db93-13b8-48b1-a36a-8588f64c3476" });
        }
    }
}
