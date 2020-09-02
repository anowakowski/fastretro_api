using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class MergetRetroBoardCardEntity_ModifyRetroBoardCardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MergetRetroBoardCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetroBoardFirebaseDocId = table.Column<string>(nullable: true),
                    RetroBoardCardFirebaseDocId = table.Column<string>(nullable: true),
                    retroBoardCardId = table.Column<int>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MergetRetroBoardCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MergetRetroBoardCards_RetroBoardCards_retroBoardCardId",
                        column: x => x.retroBoardCardId,
                        principalTable: "RetroBoardCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MergetRetroBoardCards_retroBoardCardId",
                table: "MergetRetroBoardCards",
                column: "retroBoardCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MergetRetroBoardCards");
        }
    }
}
