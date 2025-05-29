using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MVPv5.Domain.Migrations;

/// <inheritdoc />
public partial class DBalmReady : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Templates",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                Type = table.Column<string>(type: "text", nullable: true),
                DateCreation = table.Column<DateOnly>(type: "date", nullable: false),
                Content = table.Column<byte[]>(type: "bytea", nullable: false),
                ContentType = table.Column<string>(type: "text", nullable: false),
                Tags = table.Column<string[]>(type: "text[]", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Templates", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Nickname = table.Column<string>(type: "text", nullable: false),
                Login = table.Column<string>(type: "text", nullable: false),
                Password = table.Column<string>(type: "text", nullable: false),
                AccessRule = table.Column<byte>(type: "smallint", nullable: false),
                DateCreation = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Documents",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                DateCreation = table.Column<DateOnly>(type: "date", nullable: false),
                MetadataJson = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                TemplateId = table.Column<int>(type: "integer", nullable: false),
                UserId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Documents", x => x.Id);
                table.ForeignKey(
                    name: "FK_Documents_Templates_TemplateId",
                    column: x => x.TemplateId,
                    principalTable: "Templates",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Documents_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Documents_TemplateId",
            table: "Documents",
            column: "TemplateId");

        migrationBuilder.CreateIndex(
            name: "IX_Documents_UserId",
            table: "Documents",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Documents");

        migrationBuilder.DropTable(
            name: "Templates");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
