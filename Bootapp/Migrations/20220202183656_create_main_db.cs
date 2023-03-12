using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Bootapp.Migrations
{
    public partial class create_main_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_account",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    active = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_datasource",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_datasource", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_project",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    connection_string = table.Column<string>(type: "json", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    active = table.Column<int>(type: "integer", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    datasource_id = table.Column<int>(type: "integer", nullable: false),
                    accountid = table.Column<Guid>(type: "uuid", nullable: true),
                    datasourceid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_project", x => x.id);
                    table.ForeignKey(
                        name: "FK_app_project_app_account_accountid",
                        column: x => x.accountid,
                        principalTable: "app_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_app_project_app_datasource_datasourceid",
                        column: x => x.datasourceid,
                        principalTable: "app_datasource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "app_datasource",
                columns: new[] { "id", "active", "name" },
                values: new object[,]
                {
                    { 1, 1, "PostgreSQL" },
                    { 2, 0, "MySQL" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_project_accountid",
                table: "app_project",
                column: "accountid");

            migrationBuilder.CreateIndex(
                name: "IX_app_project_datasourceid",
                table: "app_project",
                column: "datasourceid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_project");

            migrationBuilder.DropTable(
                name: "app_account");

            migrationBuilder.DropTable(
                name: "app_datasource");
        }
    }
}
