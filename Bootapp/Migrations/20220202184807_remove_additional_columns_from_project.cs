using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bootapp.Migrations
{
    public partial class remove_additional_columns_from_project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "account_id",
                table: "app_project");

            migrationBuilder.DropColumn(
                name: "datasource_id",
                table: "app_project");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "account_id",
                table: "app_project",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "datasource_id",
                table: "app_project",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
