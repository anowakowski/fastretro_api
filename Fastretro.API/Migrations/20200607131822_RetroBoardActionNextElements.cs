using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class RetroBoardActionNextElements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastActionCountUpdate",
                table: "RetroBoardAdditionalInfos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RetroBoardCardFromLastUpdateForActionCountId",
                table: "RetroBoardAdditionalInfos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RetroBoardHasAction",
                table: "RetroBoardAdditionalInfos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastActionCountUpdate",
                table: "RetroBoardAdditionalInfos");

            migrationBuilder.DropColumn(
                name: "RetroBoardCardFromLastUpdateForActionCountId",
                table: "RetroBoardAdditionalInfos");

            migrationBuilder.DropColumn(
                name: "RetroBoardHasAction",
                table: "RetroBoardAdditionalInfos");
        }
    }
}
