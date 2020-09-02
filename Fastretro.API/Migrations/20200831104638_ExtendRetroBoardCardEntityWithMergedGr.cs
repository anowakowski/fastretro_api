using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class ExtendRetroBoardCardEntityWithMergedGr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RetroBoardCardMergedGroupId",
                table: "RetroBoardCards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetroBoardCards_RetroBoardCardMergedGroupId",
                table: "RetroBoardCards",
                column: "RetroBoardCardMergedGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_RetroBoardCards_RetroBoardCardMergedGroups_RetroBoardCardMergedGroupId",
                table: "RetroBoardCards",
                column: "RetroBoardCardMergedGroupId",
                principalTable: "RetroBoardCardMergedGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RetroBoardCards_RetroBoardCardMergedGroups_RetroBoardCardMergedGroupId",
                table: "RetroBoardCards");

            migrationBuilder.DropIndex(
                name: "IX_RetroBoardCards_RetroBoardCardMergedGroupId",
                table: "RetroBoardCards");

            migrationBuilder.DropColumn(
                name: "RetroBoardCardMergedGroupId",
                table: "RetroBoardCards");
        }
    }
}
