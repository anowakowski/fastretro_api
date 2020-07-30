using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class UserWaitingForApprovalEntityNextPart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprovalByCreator",
                table: "userWaitingToApproveWorkspaceJoins",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprovalByCreator",
                table: "userWaitingToApproveWorkspaceJoins");
        }
    }
}
