using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bootapp.Migrations
{
    public partial class table_settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "app_project_table_setting_id");

            migrationBuilder.CreateTable(
                name: "app_project_table_setting",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"app_project_table_setting_id\"')"),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    table_name = table.Column<string>(type: "text", nullable: false),
                    table_schema = table.Column<string>(type: "text", nullable: true),
                    create = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    update = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    read = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    delete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_project_table_setting", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_project_table_setting");

            migrationBuilder.DropSequence(
                name: "app_project_table_setting_id");
        }
    }
}
