using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class NewRetroBoardCardMergetGroupEntity_ExtendOtherEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MergetRetroBoardCards_RetroBoardCards_retroBoardCardId",
                table: "MergetRetroBoardCards");

            migrationBuilder.DropColumn(
                name: "RetroBoardCardFirebaseDocId",
                table: "MergetRetroBoardCards");

            migrationBuilder.DropColumn(
                name: "RetroBoardFirebaseDocId",
                table: "MergetRetroBoardCards");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "MergetRetroBoardCards");

            migrationBuilder.RenameColumn(
                name: "retroBoardCardId",
                table: "MergetRetroBoardCards",
                newName: "RetroBoardCardId");

            migrationBuilder.RenameIndex(
                name: "IX_MergetRetroBoardCards_retroBoardCardId",
                table: "MergetRetroBoardCards",
                newName: "IX_MergetRetroBoardCards_RetroBoardCardId");

            migrationBuilder.AddColumn<bool>(
                name: "isMerged",
                table: "RetroBoardCards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "RetroBoardCardId",
                table: "MergetRetroBoardCards",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetroBoardCardMergedGroupId",
                table: "MergetRetroBoardCards",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RetroBoardCardMergedGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetroBoardCardMergedGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MergetRetroBoardCards_RetroBoardCardMergedGroupId",
                table: "MergetRetroBoardCards",
                column: "RetroBoardCardMergedGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_MergetRetroBoardCards_RetroBoardCards_RetroBoardCardId",
                table: "MergetRetroBoardCards",
                column: "RetroBoardCardId",
                principalTable: "RetroBoardCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MergetRetroBoardCards_RetroBoardCardMergedGroups_RetroBoardCardMergedGroupId",
                table: "MergetRetroBoardCards",
                column: "RetroBoardCardMergedGroupId",
                principalTable: "RetroBoardCardMergedGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MergetRetroBoardCards_RetroBoardCards_RetroBoardCardId",
                table: "MergetRetroBoardCards");

            migrationBuilder.DropForeignKey(
                name: "FK_MergetRetroBoardCards_RetroBoardCardMergedGroups_RetroBoardCardMergedGroupId",
                table: "MergetRetroBoardCards");

            migrationBuilder.DropTable(
                name: "RetroBoardCardMergedGroups");

            migrationBuilder.DropIndex(
                name: "IX_MergetRetroBoardCards_RetroBoardCardMergedGroupId",
                table: "MergetRetroBoardCards");

            migrationBuilder.DropColumn(
                name: "isMerged",
                table: "RetroBoardCards");

            migrationBuilder.DropColumn(
                name: "RetroBoardCardMergedGroupId",
                table: "MergetRetroBoardCards");

            migrationBuilder.RenameColumn(
                name: "RetroBoardCardId",
                table: "MergetRetroBoardCards",
                newName: "retroBoardCardId");

            migrationBuilder.RenameIndex(
                name: "IX_MergetRetroBoardCards_RetroBoardCardId",
                table: "MergetRetroBoardCards",
                newName: "IX_MergetRetroBoardCards_retroBoardCardId");

            migrationBuilder.AlterColumn<int>(
                name: "retroBoardCardId",
                table: "MergetRetroBoardCards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "RetroBoardCardFirebaseDocId",
                table: "MergetRetroBoardCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RetroBoardFirebaseDocId",
                table: "MergetRetroBoardCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "MergetRetroBoardCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MergetRetroBoardCards_RetroBoardCards_retroBoardCardId",
                table: "MergetRetroBoardCards",
                column: "retroBoardCardId",
                principalTable: "RetroBoardCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
