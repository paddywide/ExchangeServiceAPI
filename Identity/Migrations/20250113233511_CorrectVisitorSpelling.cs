using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeRate.Identity.Migrations
{
    /// <inheritdoc />
    public partial class CorrectVisitorSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "9cf9b4ff-6a21-45f6-8d27-cb2937d5d44b", "visitor@localhost.com", "VISITOR@LOCALHOST.COM", "VISITOR@LOCALHOST.COM", "AQAAAAIAAYagAAAAEBuJktN8YO5kHUijOFqyJTZ7apJXeQWNLQ3kgXutIfL9BYQGhOpv9bATskNgNf5QiA==", "6f93db93-13b8-48b1-a36a-8588f64c3476", "visitor@localhost.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "636b0546-0fda-4001-a25b-591e1b6aeb7c", "AQAAAAIAAYagAAAAEGHt+VtzkdP1+yQJNdfTISRSdm5HQQyBwzCWwbWIP50EWbFSaoVDqvHjgMpJ9LjpMQ==", "800dcde3-f8b4-4a53-bbc3-b44476a81ea3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "0ae047b4-a7fd-4ee8-919d-2f2ed6c27c71", "vistor@localhost.com", "VISTOR@LOCALHOST.COM", "VISTOR@LOCALHOST.COM", "AQAAAAIAAYagAAAAEHuuP43zyhm+NABJDiYZmiR971Vqdy3CNwRV9SgsCxG0HzJ4LJ4+d2vjN2Ut9HywBA==", "10dc3ec4-3df3-4237-bec1-4013a614e3f7", "vistor@localhost.com" });
        }
    }
}
