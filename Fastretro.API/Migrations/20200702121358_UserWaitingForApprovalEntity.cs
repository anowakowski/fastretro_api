using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class UserWaitingForApprovalEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userWaitingToApproveWorkspaceJoins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkspceWithRequiredAccessFirebaseId = table.Column<string>(nullable: true),
                    CreatorUserFirebaseId = table.Column<string>(nullable: true),
                    UserWantToJoinFirebaseId = table.Column<string>(nullable: true),
                    RequestIsApprove = table.Column<bool>(nullable: false),
                    LastModifyDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userWaitingToApproveWorkspaceJoins", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userWaitingToApproveWorkspaceJoins");
        }
    }
}
