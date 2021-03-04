using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Resource.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Gener = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Gener", "Title", "Year" },
                values: new object[] { new Guid("e83b4bec-765b-4e13-a349-611ad5cda0cb"), "Юмористическое фэнтези", "Мор, ученик Смерти", new DateTime(1987, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
