using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class AuthModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("cdee6b90-d530-4e77-8980-5a845a88806a"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEDlQwlnqGnsCfPMt7c3lpGKPFiW4hep1XYJMIQcG7SJmcL9ZZihi5jiQOtZSXnEUOg==");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "PasswordHash", "RoleId", "UserName" },
                values: new object[] { new Guid("c09c1ed7-ab92-46be-b5a9-d11741508b0c"), "System.Byte[]", 1, "Makesh1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c09c1ed7-ab92-46be-b5a9-d11741508b0c"));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("cdee6b90-d530-4e77-8980-5a845a88806a"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELCnT3Dkbl6jJBp+qX6ltZr2YibMhvo2f0yLVcVgCFY5/Jr5ZlPb+mQLpU30tTkTRQ==");
        }
    }
}
