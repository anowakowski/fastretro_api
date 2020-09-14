using Microsoft.EntityFrameworkCore.Migrations;

namespace Fastretro.API.Migrations
{
    public partial class ActionCardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RetroBoardActionCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetroBoardFirebaseDocId = table.Column<string>(nullable: true),
                    RetroBoardCardFirebaseDocId = table.Column<string>(nullable: true),
                    RetroBoardActionCardFirebaseDocId = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetroBoardActionCards", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetroBoardActionCards");
        }
    }
}
