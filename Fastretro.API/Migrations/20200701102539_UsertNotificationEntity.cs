using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class UsertNotificationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotyficationType = table.Column<string>(nullable: true),
                    WorkspceWithRequiredAccessFirebaseId = table.Column<string>(nullable: true),
                    CreatorUserFirebaseId = table.Column<string>(nullable: true),
                    UserWantToJoinFirebaseId = table.Column<string>(nullable: true),
                    CreatonDate = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userNotifications", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userNotifications");
        }
    }
}
