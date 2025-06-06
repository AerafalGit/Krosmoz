using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Krosmoz.Servers.GameServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Interactives : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "interactives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    GfxId = table.Column<int>(type: "integer", nullable: false),
                    Animated = table.Column<bool>(type: "boolean", nullable: false),
                    MapId = table.Column<int>(type: "integer", nullable: false),
                    ElementId = table.Column<int>(type: "integer", nullable: false),
                    MapsData = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interactives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "interactives_actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    InteractiveId = table.Column<int>(type: "integer", nullable: false),
                    InteractiveTemplateId = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<int>(type: "integer", nullable: false),
                    NameId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Parameters = table.Column<string[]>(type: "text[]", nullable: true),
                    Criterion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interactives_actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_interactives_actions_interactives_InteractiveId",
                        column: x => x.InteractiveId,
                        principalTable: "interactives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_interactives_actions_InteractiveId",
                table: "interactives_actions",
                column: "InteractiveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "interactives_actions");

            migrationBuilder.DropTable(
                name: "interactives");
        }
    }
}
