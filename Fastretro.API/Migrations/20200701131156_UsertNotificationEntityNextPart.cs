using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class UsertNotificationEntityNextPart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserFirebaseId",
                table: "userNotifications");

            migrationBuilder.DropColumn(
                name: "UserWantToJoinFirebaseId",
                table: "userNotifications");

            migrationBuilder.DropColumn(
                name: "WorkspceWithRequiredAccessFirebaseId",
                table: "userNotifications");

            migrationBuilder.CreateTable(
                name: "UserNotificationWorkspaceWithRequiredAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkspceWithRequiredAccessFirebaseId = table.Column<string>(nullable: true),
                    CreatorUserFirebaseId = table.Column<string>(nullable: true),
                    UserWantToJoinFirebaseId = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    UserNotificationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotificationWorkspaceWithRequiredAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotificationWorkspaceWithRequiredAccesses_userNotifications_UserNotificationId",
                        column: x => x.UserNotificationId,
                        principalTable: "userNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationWorkspaceWithRequiredAccesses_UserNotificationId",
                table: "UserNotificationWorkspaceWithRequiredAccesses",
                column: "UserNotificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserNotificationWorkspaceWithRequiredAccesses");

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserFirebaseId",
                table: "userNotifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserWantToJoinFirebaseId",
                table: "userNotifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkspceWithRequiredAccessFirebaseId",
                table: "userNotifications",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
