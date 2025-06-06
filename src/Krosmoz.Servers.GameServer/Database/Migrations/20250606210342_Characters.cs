using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Krosmoz.Servers.GameServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Characters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MountXp",
                table: "experiences",
                type: "numeric(20,0)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "JobXp",
                table: "experiences",
                type: "numeric(20,0)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GuildXp",
                table: "experiences",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "CharacterXp",
                table: "experiences",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "AlignmentHonor",
                table: "experiences",
                type: "numeric(20,0)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
                    Look = table.Column<string>(type: "text", nullable: false),
                    Kamas = table.Column<long>(type: "bigint", nullable: false),
                    Emotes = table.Column<int[]>(type: "integer[]", nullable: false),
                    Spells = table.Column<int[]>(type: "integer[]", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeathCount = table.Column<int>(type: "integer", nullable: false),
                    DeathMaxLevel = table.Column<byte>(type: "smallint", nullable: false),
                    DeathState = table.Column<int>(type: "integer", nullable: false),
                    Characteristics = table.Column<byte[]>(type: "bytea", nullable: false),
                    PossibleChanges = table.Column<int>(type: "integer", nullable: false),
                    MandatoryChanges = table.Column<int>(type: "integer", nullable: false),
                    Capabilities = table.Column<int>(type: "integer", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_characters_Name",
                table: "characters",
                column: "Name",
                unique: true)
                .Annotation("Relational:Collation", new[] { "case_insensitive" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "characters");

            migrationBuilder.AlterColumn<long>(
                name: "MountXp",
                table: "experiences",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "JobXp",
                table: "experiences",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GuildXp",
                table: "experiences",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AlterColumn<long>(
                name: "CharacterXp",
                table: "experiences",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AlterColumn<long>(
                name: "AlignmentHonor",
                table: "experiences",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)",
                oldNullable: true);
        }
    }
}
