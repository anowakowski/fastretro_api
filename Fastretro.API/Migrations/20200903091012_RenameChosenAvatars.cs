using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class RenameChosenAvatars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChosenAvatarUrl",
                table: "UsersInTeams");

            migrationBuilder.DropColumn(
                name: "ChosenAvatarUrl",
                table: "FirebaseUsersData");

            migrationBuilder.AddColumn<string>(
                name: "ChosenAvatarName",
                table: "UsersInTeams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChosenAvatarName",
                table: "FirebaseUsersData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChosenAvatarName",
                table: "UsersInTeams");

            migrationBuilder.DropColumn(
                name: "ChosenAvatarName",
                table: "FirebaseUsersData");

            migrationBuilder.AddColumn<string>(
                name: "ChosenAvatarUrl",
                table: "UsersInTeams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChosenAvatarUrl",
                table: "FirebaseUsersData",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
