using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class RetroBoardAdditionalInfoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RetroBoardAdditionalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetroBoardFirebaseDocId = table.Column<string>(nullable: true),
                    TeamFirebaseDocId = table.Column<string>(nullable: true),
                    WorkspaceFirebaseDocId = table.Column<string>(nullable: true),
                    RetroBoardIndexCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetroBoardAdditionalInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetroBoardAdditionalInfos");
        }
    }
}
