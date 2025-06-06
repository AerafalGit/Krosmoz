using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Krosmoz.Servers.GameServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Maps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "maps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Outdoor = table.Column<bool>(type: "boolean", nullable: false),
                    Capabilities = table.Column<long>(type: "bigint", nullable: false),
                    SubAreaId = table.Column<int>(type: "integer", nullable: false),
                    WorldMap = table.Column<int>(type: "integer", nullable: false),
                    HasPriorityOnWorldMap = table.Column<bool>(type: "boolean", nullable: false),
                    PrismAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    PvpDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    PlacementGenDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    MerchantsMax = table.Column<int>(type: "integer", nullable: false),
                    SpawnDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    RedCells = table.Column<short[]>(type: "smallint[]", nullable: false),
                    BlueCells = table.Column<short[]>(type: "smallint[]", nullable: false),
                    Cells = table.Column<byte[]>(type: "bytea", nullable: false),
                    TopNeighbourId = table.Column<int>(type: "integer", nullable: false),
                    BottomNeighbourId = table.Column<int>(type: "integer", nullable: false),
                    LeftNeighbourId = table.Column<int>(type: "integer", nullable: false),
                    RightNeighbourId = table.Column<int>(type: "integer", nullable: false),
                    TopCellId = table.Column<short>(type: "smallint", nullable: true),
                    BottomCellId = table.Column<short>(type: "smallint", nullable: true),
                    LeftCellId = table.Column<short>(type: "smallint", nullable: true),
                    RightCellId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_maps", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_interactives_MapId",
                table: "interactives",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_interactives_maps_MapId",
                table: "interactives",
                column: "MapId",
                principalTable: "maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_interactives_maps_MapId",
                table: "interactives");

            migrationBuilder.DropTable(
                name: "maps");

            migrationBuilder.DropIndex(
                name: "IX_interactives_MapId",
                table: "interactives");
        }
    }
}
