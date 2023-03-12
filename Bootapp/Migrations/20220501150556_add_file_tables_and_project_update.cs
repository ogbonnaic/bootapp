using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bootapp.Migrations
{
    public partial class add_file_tables_and_project_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "app_file_type_id");

            migrationBuilder.AddColumn<string>(
                name: "api_key",
                table: "app_project",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "api_secret",
                table: "app_project",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "enable_auth",
                table: "app_project",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "app_file",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "text", nullable: false),
                    short_url = table.Column<string>(type: "text", nullable: false),
                    file_size = table.Column<decimal>(type: "numeric", nullable: false),
                    file_type_id = table.Column<int>(type: "integer", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_file", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_file_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"app_file_type_id\"')"),
                    format = table.Column<string>(type: "text", nullable: false),
                    extension = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_file_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_short_url",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    short_url = table.Column<string>(type: "text", nullable: true),
                    original_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_short_url", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_file");

            migrationBuilder.DropTable(
                name: "app_file_type");

            migrationBuilder.DropTable(
                name: "app_short_url");

            migrationBuilder.DropSequence(
                name: "app_file_type_id");

            migrationBuilder.DropColumn(
                name: "api_key",
                table: "app_project");

            migrationBuilder.DropColumn(
                name: "api_secret",
                table: "app_project");

            migrationBuilder.DropColumn(
                name: "enable_auth",
                table: "app_project");
        }
    }
}
