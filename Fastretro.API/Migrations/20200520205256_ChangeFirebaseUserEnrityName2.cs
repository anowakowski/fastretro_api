using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class ChangeFirebaseUserEnrityName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirebaseUsers_CurrentUserInRetroBoards_CurrentUserInRetroBoardId",
                table: "FirebaseUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FirebaseUsers",
                table: "FirebaseUsers");

            migrationBuilder.RenameTable(
                name: "FirebaseUsers",
                newName: "FirebaseUsersData");

            migrationBuilder.RenameIndex(
                name: "IX_FirebaseUsers_CurrentUserInRetroBoardId",
                table: "FirebaseUsersData",
                newName: "IX_FirebaseUsersData_CurrentUserInRetroBoardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FirebaseUsersData",
                table: "FirebaseUsersData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FirebaseUsersData_CurrentUserInRetroBoards_CurrentUserInRetroBoardId",
                table: "FirebaseUsersData",
                column: "CurrentUserInRetroBoardId",
                principalTable: "CurrentUserInRetroBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirebaseUsersData_CurrentUserInRetroBoards_CurrentUserInRetroBoardId",
                table: "FirebaseUsersData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FirebaseUsersData",
                table: "FirebaseUsersData");

            migrationBuilder.RenameTable(
                name: "FirebaseUsersData",
                newName: "FirebaseUsers");

            migrationBuilder.RenameIndex(
                name: "IX_FirebaseUsersData_CurrentUserInRetroBoardId",
                table: "FirebaseUsers",
                newName: "IX_FirebaseUsers_CurrentUserInRetroBoardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FirebaseUsers",
                table: "FirebaseUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FirebaseUsers_CurrentUserInRetroBoards_CurrentUserInRetroBoardId",
                table: "FirebaseUsers",
                column: "CurrentUserInRetroBoardId",
                principalTable: "CurrentUserInRetroBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
