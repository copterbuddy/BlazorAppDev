using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAppDev.Server.Migrations
{
    public partial class AddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "UserDetail",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 4, 8, 16, 37, 50, 904, DateTimeKind.Local).AddTicks(5215),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "CONVERT(DATETIME2, SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "UserDetail",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 4, 8, 16, 37, 50, 904, DateTimeKind.Local).AddTicks(5067),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("ab5364a3-ed28-4a09-9434-38fedb7059d4"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "USER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserDetail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "UserDetail",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "CONVERT(DATETIME2, SYSUTCDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 4, 8, 16, 37, 50, 904, DateTimeKind.Local).AddTicks(5215));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "UserDetail",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 4, 8, 16, 37, 50, 904, DateTimeKind.Local).AddTicks(5067));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("ab5364a3-ed28-4a09-9434-38fedb7059d4"));
        }
    }
}
