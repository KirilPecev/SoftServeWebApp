using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Data.Migrations
{
    public partial class AdminIdAddedToTheEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_AdminId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_AdminId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AdminId1",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Events",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Events_AdminId",
                table: "Events",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_AdminId",
                table: "Events",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_AdminId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_AdminId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "Events",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

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
    }
}
