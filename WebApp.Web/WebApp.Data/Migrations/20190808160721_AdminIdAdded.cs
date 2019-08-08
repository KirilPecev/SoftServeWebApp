using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Data.Migrations
{
    public partial class AdminIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AdminId1",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_AdminId1",
                table: "Events",
                column: "AdminId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_AdminId1",
                table: "Events",
                column: "AdminId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_AdminId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_AdminId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AdminId1",
                table: "Events");
        }
    }
}
