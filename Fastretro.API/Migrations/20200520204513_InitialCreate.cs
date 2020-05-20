using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentUserInRetroBoards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetroBoardId = table.Column<string>(nullable: true),
                    FireBaseUserData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentUserInRetroBoards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FirebaseUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirebaseUserDocId = table.Column<string>(nullable: true),
                    CurrentUserInRetroBoardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirebaseUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirebaseUsers_CurrentUserInRetroBoards_CurrentUserInRetroBoardId",
                        column: x => x.CurrentUserInRetroBoardId,
                        principalTable: "CurrentUserInRetroBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FirebaseUsers_CurrentUserInRetroBoardId",
                table: "FirebaseUsers",
                column: "CurrentUserInRetroBoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirebaseUsers");

            migrationBuilder.DropTable(
                name: "CurrentUserInRetroBoards");
        }
    }
}
