using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class ExpandUserNotificationWorkspaceWithRequiredAccessResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccessResponses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationWorkspaceWithRequiredAccessResponses_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccessResponses",
                column: "UserWaitingToApproveWorkspaceJoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotificationWorkspaceWithRequiredAccessResponses_userWaitingToApproveWorkspaceJoins_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccessResponses",
                column: "UserWaitingToApproveWorkspaceJoinId",
                principalTable: "userWaitingToApproveWorkspaceJoins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotificationWorkspaceWithRequiredAccessResponses_userWaitingToApproveWorkspaceJoins_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccessResponses");

            migrationBuilder.DropIndex(
                name: "IX_UserNotificationWorkspaceWithRequiredAccessResponses_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccessResponses");

            migrationBuilder.DropColumn(
                name: "UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccessResponses");
        }
    }
}
