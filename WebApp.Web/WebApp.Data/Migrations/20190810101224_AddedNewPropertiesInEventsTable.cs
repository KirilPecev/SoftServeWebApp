using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Data.Migrations
{
    public partial class AddedNewPropertiesInEventsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");
        }
    }
}
