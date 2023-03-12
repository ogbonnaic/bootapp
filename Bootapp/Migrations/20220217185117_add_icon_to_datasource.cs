using Microsoft.EntityFrameworkCore.Migrations;

namespace Bootapp.Migrations
{
    public partial class add_icon_to_datasource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "app_datasource",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "icon",
                table: "app_datasource");
        }
    }
}
