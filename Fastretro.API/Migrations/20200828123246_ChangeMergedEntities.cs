using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class ChangeMergedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MergetRetroBoardCards");

            migrationBuilder.CreateTable(
                name: "MergedRetroBoardCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetroBoardCardMergedGroupId = table.Column<int>(nullable: true),
                    RetroBoardCardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MergedRetroBoardCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MergedRetroBoardCards_RetroBoardCards_RetroBoardCardId",
                        column: x => x.RetroBoardCardId,
                        principalTable: "RetroBoardCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MergedRetroBoardCards_RetroBoardCardMergedGroups_RetroBoardCardMergedGroupId",
                        column: x => x.RetroBoardCardMergedGroupId,
                        principalTable: "RetroBoardCardMergedGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MergedRetroBoardCards_RetroBoardCardId",
                table: "MergedRetroBoardCards",
                column: "RetroBoardCardId");

            migrationBuilder.CreateIndex(
                name: "IX_MergedRetroBoardCards_RetroBoardCardMergedGroupId",
                table: "MergedRetroBoardCards",
                column: "RetroBoardCardMergedGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MergedRetroBoardCards");

            migrationBuilder.CreateTable(
                name: "MergetRetroBoardCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetroBoardCardId = table.Column<int>(type: "int", nullable: false),
                    RetroBoardCardMergedGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MergetRetroBoardCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MergetRetroBoardCards_RetroBoardCards_RetroBoardCardId",
                        column: x => x.RetroBoardCardId,
                        principalTable: "RetroBoardCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MergetRetroBoardCards_RetroBoardCardMergedGroups_RetroBoardCardMergedGroupId",
                        column: x => x.RetroBoardCardMergedGroupId,
                        principalTable: "RetroBoardCardMergedGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MergetRetroBoardCards_RetroBoardCardId",
                table: "MergetRetroBoardCards",
                column: "RetroBoardCardId");

            migrationBuilder.CreateIndex(
                name: "IX_MergetRetroBoardCards_RetroBoardCardMergedGroupId",
                table: "MergetRetroBoardCards",
                column: "RetroBoardCardMergedGroupId");
        }
    }
}
