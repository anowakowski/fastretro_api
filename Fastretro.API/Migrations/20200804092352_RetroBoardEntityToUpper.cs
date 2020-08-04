using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class RetroBoardEntityToUpper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_retroBoards",
                table: "retroBoards");

            migrationBuilder.RenameTable(
                name: "retroBoards",
                newName: "RetroBoards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RetroBoards",
                table: "RetroBoards",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RetroBoards",
                table: "RetroBoards");

            migrationBuilder.RenameTable(
                name: "RetroBoards",
                newName: "retroBoards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_retroBoards",
                table: "retroBoards",
                column: "Id");
        }
    }
}
