using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MVPv5.Domain.Migrations
{
    /// <inheritdoc />
    public partial class NewDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableDocuments_TableUsers_UserEntityId",
                table: "TableDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableUsers",
                table: "TableUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableDocuments",
                table: "TableDocuments");

            migrationBuilder.DropIndex(
                name: "IX_TableDocuments_UserEntityId",
                table: "TableDocuments");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "TableDocuments");

            migrationBuilder.RenameTable(
                name: "TableUsers",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "TableDocuments",
                newName: "Documents");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateCreation",
                table: "Documents",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<JsonDocument>(
                name: "MetadataJson",
                table: "Documents",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Documents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Documents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    DateCreation = table.Column<DateOnly>(type: "date", nullable: false),
                    Content = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DateCreation",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "MetadataJson",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "TableUsers");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "TableDocuments");

            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "TableDocuments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableUsers",
                table: "TableUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableDocuments",
                table: "TableDocuments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TableDocuments_UserEntityId",
                table: "TableDocuments",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableDocuments_TableUsers_UserEntityId",
                table: "TableDocuments",
                column: "UserEntityId",
                principalTable: "TableUsers",
                principalColumn: "Id");
        }
    }
}
