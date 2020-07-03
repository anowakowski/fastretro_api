using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class UserWaitingForApprovalEntityNextPart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationWorkspaceWithRequiredAccesses_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses",
                column: "UserWaitingToApproveWorkspaceJoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotificationWorkspaceWithRequiredAccesses_userWaitingToApproveWorkspaceJoins_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses",
                column: "UserWaitingToApproveWorkspaceJoinId",
                principalTable: "userWaitingToApproveWorkspaceJoins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotificationWorkspaceWithRequiredAccesses_userWaitingToApproveWorkspaceJoins_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses");

            migrationBuilder.DropIndex(
                name: "IX_UserNotificationWorkspaceWithRequiredAccesses_UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses");

            migrationBuilder.DropColumn(
                name: "UserWaitingToApproveWorkspaceJoinId",
                table: "UserNotificationWorkspaceWithRequiredAccesses");
        }
    }
}
