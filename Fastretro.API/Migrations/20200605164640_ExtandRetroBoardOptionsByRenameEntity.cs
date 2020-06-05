using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class ExtandRetroBoardOptionsByRenameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShouldHideVoutCountInretroBoardCard",
                table: "RetroBoardOptions",
                newName: "ShouldHideVoutCountInRetroBoardCard");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShouldHideVoutCountInRetroBoardCard",
                table: "RetroBoardOptions",
                newName: "ShouldHideVoutCountInretroBoardCard");
        }
    }
}
