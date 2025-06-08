using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Krosmoz.Servers.GameServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:case_insensitive", "en-u-ks-primary,en-u-ks-primary,icu,False");

            migrationBuilder.CreateTable(
                name: "characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    Experience = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Breed = table.Column<int>(type: "integer", nullable: false),
                    Head = table.Column<int>(type: "integer", nullable: false),
                    Sex = table.Column<bool>(type: "boolean", nullable: false),
                    Position = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StatusMessage = table.Column<string>(type: "text", nullable: true),
                    Look = table.Column<string>(type: "text", nullable: false),
                    Kamas = table.Column<int>(type: "integer", nullable: false),
                    Emotes = table.Column<int[]>(type: "integer[]", nullable: false),
                    Spells = table.Column<int[]>(type: "integer[]", nullable: false),
                    GeneralShortcutBar = table.Column<byte[]>(type: "bytea", nullable: false),
                    SpellShortcutBar = table.Column<byte[]>(type: "bytea", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeathCount = table.Column<int>(type: "integer", nullable: false),
                    DeathMaxLevel = table.Column<byte>(type: "smallint", nullable: false),
                    DeathState = table.Column<int>(type: "integer", nullable: false),
                    AlignmentSide = table.Column<int>(type: "integer", nullable: false),
                    AlignmentValue = table.Column<short>(type: "smallint", nullable: false),
                    AlignmentHonor = table.Column<int>(type: "integer", nullable: false),
                    AlignmentStatus = table.Column<int>(type: "integer", nullable: false),
                    Characteristics = table.Column<byte[]>(type: "bytea", nullable: false),
                    PossibleChanges = table.Column<int>(type: "integer", nullable: false),
                    MandatoryChanges = table.Column<int>(type: "integer", nullable: false),
                    Restrictions = table.Column<int>(type: "integer", nullable: false),
                    SpouseId = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "experiences",
                columns: table => new
                {
                    Level = table.Column<byte>(type: "smallint", nullable: false),
                    CharacterXp = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    GuildXp = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    JobXp = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    MountXp = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    AlignmentHonor = table.Column<decimal>(type: "numeric(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_experiences", x => x.Level);
                });

            migrationBuilder.CreateTable(
                name: "items_appearances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    AppearanceId = table.Column<int>(type: "integer", nullable: false),
                    CustomLook = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items_appearances", x => x.Id);
                });

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
                    table.ForeignKey(
                        name: "FK_interactives_maps_MapId",
                        column: x => x.MapId,
                        principalTable: "maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_characters_Name",
                table: "characters",
                column: "Name",
                unique: true)
                .Annotation("Relational:Collation", new[] { "case_insensitive" });

            migrationBuilder.CreateIndex(
                name: "IX_interactives_MapId",
                table: "interactives",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_interactives_actions_InteractiveId",
                table: "interactives_actions",
                column: "InteractiveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "characters");

            migrationBuilder.DropTable(
                name: "experiences");

            migrationBuilder.DropTable(
                name: "interactives_actions");

            migrationBuilder.DropTable(
                name: "items_appearances");

            migrationBuilder.DropTable(
                name: "interactives");

            migrationBuilder.DropTable(
                name: "maps");
        }
    }
}
