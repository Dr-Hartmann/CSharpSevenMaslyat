using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVPv4.Migrations
{
    /// <inheritdoc />
    public partial class aotestv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Year",
                table: "DocumentV1",
                type: "date",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Year",
                table: "DocumentV1",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
