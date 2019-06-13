using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.DAL.Migrations
{
    public partial class RowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Populii",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Notes",
                nullable: false,
                defaultValue: new DateTime(2019, 6, 13, 14, 29, 23, 576, DateTimeKind.Local).AddTicks(4581),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 11, 22, 14, 53, 713, DateTimeKind.Local).AddTicks(8878));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Populii");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Notes",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 11, 22, 14, 53, 713, DateTimeKind.Local).AddTicks(8878),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 6, 13, 14, 29, 23, 576, DateTimeKind.Local).AddTicks(4581));
        }
    }
}
