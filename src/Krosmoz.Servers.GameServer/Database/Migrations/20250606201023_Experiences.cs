using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Krosmoz.Servers.GameServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Experiences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:case_insensitive", "en-u-ks-primary,en-u-ks-primary,icu,False");

            migrationBuilder.CreateTable(
                name: "experiences",
                columns: table => new
                {
                    Level = table.Column<byte>(type: "smallint", nullable: false),
                    CharacterXp = table.Column<long>(type: "bigint", nullable: false),
                    GuildXp = table.Column<long>(type: "bigint", nullable: false),
                    JobXp = table.Column<long>(type: "bigint", nullable: true),
                    MountXp = table.Column<long>(type: "bigint", nullable: true),
                    AlignmentHonor = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_experiences", x => x.Level);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "experiences");
        }
    }
}
