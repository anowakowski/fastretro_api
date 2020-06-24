using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class RetroBoardStatusEntityNext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RetroBoardStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetroBoardFirebaseDocId = table.Column<string>(nullable: true),
                    TeamFirebaseDocId = table.Column<string>(nullable: true),
                    WorkspaceFirebaseDocId = table.Column<string>(nullable: true),
                    IsFinished = table.Column<bool>(nullable: false),
                    IsStarted = table.Column<bool>(nullable: false),
                    LastModifyDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetroBoardStatuses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetroBoardStatuses");
        }
    }
}
