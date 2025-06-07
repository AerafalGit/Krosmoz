using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Krosmoz.Servers.GameServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class CharactersAligments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capabilities",
                table: "characters",
                newName: "Restrictions");

            migrationBuilder.AlterColumn<int>(
                name: "Kamas",
                table: "characters",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "AlignmentHonor",
                table: "characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AlignmentSide",
                table: "characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AlignmentStatus",
                table: "characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "AlignmentValue",
                table: "characters",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<byte[]>(
                name: "GeneralShortcutBar",
                table: "characters",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "SpellShortcutBar",
                table: "characters",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "SpouseId",
                table: "characters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "characters",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlignmentHonor",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "AlignmentSide",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "AlignmentStatus",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "AlignmentValue",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "GeneralShortcutBar",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "SpellShortcutBar",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "SpouseId",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "characters");

            migrationBuilder.RenameColumn(
                name: "Restrictions",
                table: "characters",
                newName: "Capabilities");

            migrationBuilder.AlterColumn<long>(
                name: "Kamas",
                table: "characters",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
