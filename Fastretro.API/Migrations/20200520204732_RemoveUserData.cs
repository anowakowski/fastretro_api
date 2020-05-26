using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class RemoveUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FireBaseUserData",
                table: "CurrentUserInRetroBoards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FireBaseUserData",
                table: "CurrentUserInRetroBoards",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
