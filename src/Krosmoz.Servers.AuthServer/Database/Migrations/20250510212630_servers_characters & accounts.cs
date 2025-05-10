using System;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Krosmoz.Servers.AuthServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class servers_charactersaccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Hierarchy = table.Column<string>(type: "text", nullable: false),
                    SecretQuestion = table.Column<string>(type: "text", nullable: false),
                    SecretAnswer = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<IPAddress>(type: "inet", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "servers_characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServerId = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    CharacterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servers_characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_servers_characters_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_Username_Nickname",
                table: "accounts",
                columns: new[] { "Username", "Nickname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_servers_characters_AccountId",
                table: "servers_characters",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_servers_characters_ServerId_AccountId",
                table: "servers_characters",
                columns: new[] { "ServerId", "AccountId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "servers_characters");

            migrationBuilder.DropTable(
                name: "accounts");
        }
    }
}
