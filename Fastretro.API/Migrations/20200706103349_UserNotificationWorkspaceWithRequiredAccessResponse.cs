using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class UserNotificationWorkspaceWithRequiredAccessResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotificationWorkspaceWithRequiredAccesses_userWaitingToApproveWorkspaceJoins_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses");

            migrationBuilder.AlterColumn<int>(
                name: "UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserNotificationWorkspaceWithRequiredAccessResponses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkspceWithRequiredAccessFirebaseId = table.Column<string>(nullable: true),
                    UserJoinedToWorkspaceFirebaseId = table.Column<string>(nullable: true),
                    WorkspaceName = table.Column<string>(nullable: true),
                    UserNotificationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotificationWorkspaceWithRequiredAccessResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotificationWorkspaceWithRequiredAccessResponses_userNotifications_UserNotificationId",
                        column: x => x.UserNotificationId,
                        principalTable: "userNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationWorkspaceWithRequiredAccessResponses_UserNotificationId",
                table: "UserNotificationWorkspaceWithRequiredAccessResponses",
                column: "UserNotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotificationWorkspaceWithRequiredAccesses_userWaitingToApproveWorkspaceJoins_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses",
                column: "UserWaitingToApproveWorkspaceJoinId",
                principalTable: "userWaitingToApproveWorkspaceJoins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotificationWorkspaceWithRequiredAccesses_userWaitingToApproveWorkspaceJoins_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses");

            migrationBuilder.DropTable(
                name: "UserNotificationWorkspaceWithRequiredAccessResponses");

            migrationBuilder.AlterColumn<int>(
                name: "UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotificationWorkspaceWithRequiredAccesses_userWaitingToApproveWorkspaceJoins_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses",
                column: "UserWaitingToApproveWorkspaceJoinId",
                principalTable: "userWaitingToApproveWorkspaceJoins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
