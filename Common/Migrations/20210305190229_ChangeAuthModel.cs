using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class ChangeAuthModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c09c1ed7-ab92-46be-b5a9-d11741508b0c"),
                column: "PasswordHash",
                value: "0A8C5453D9FDDE6B35D69C7D3FF1EFBD64DF880E354AE4E1A1831AABA958CF56");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("cdee6b90-d530-4e77-8980-5a845a88806a"),
                column: "PasswordHash",
                value: "89E01536AC207279409D4DE1E5253E01F4A1769E696DB0D6062CA9B8F56767C8");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c09c1ed7-ab92-46be-b5a9-d11741508b0c"),
                column: "PasswordHash",
                value: "System.Byte[]");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("cdee6b90-d530-4e77-8980-5a845a88806a"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEDlQwlnqGnsCfPMt7c3lpGKPFiW4hep1XYJMIQcG7SJmcL9ZZihi5jiQOtZSXnEUOg==");
        }
    }
}
