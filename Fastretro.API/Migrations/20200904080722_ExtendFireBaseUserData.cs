using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class ExtendFireBaseUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "FirebaseUsersData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "FirebaseUsersData");
        }
    }
}
