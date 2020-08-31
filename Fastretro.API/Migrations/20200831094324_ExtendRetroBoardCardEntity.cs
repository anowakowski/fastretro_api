using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class ExtendRetroBoardCardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isMerged",
                table: "RetroBoardCards",
                newName: "IsMerged");

            migrationBuilder.AddColumn<bool>(
                name: "IsHidenMergedChild",
                table: "RetroBoardCards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShowMergedParent",
                table: "RetroBoardCards",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidenMergedChild",
                table: "RetroBoardCards");

            migrationBuilder.DropColumn(
                name: "IsShowMergedParent",
                table: "RetroBoardCards");

            migrationBuilder.RenameColumn(
                name: "IsMerged",
                table: "RetroBoardCards",
                newName: "isMerged");
        }
    }
}
