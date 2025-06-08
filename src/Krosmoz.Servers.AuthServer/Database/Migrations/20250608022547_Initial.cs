using System;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Krosmoz.Servers.AuthServer.Database.Migrations
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
                name: "accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Hierarchy = table.Column<int>(type: "integer", nullable: false),
                    SecretQuestion = table.Column<string>(type: "text", nullable: false),
                    SecretAnswer = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SubscriptionStartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SubscriptionExpiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Nickname = table.Column<string>(type: "text", nullable: true),
                    MacAddress = table.Column<PhysicalAddress>(type: "macaddr", nullable: true),
                    IpAddress = table.Column<IPAddress>(type: "inet", nullable: true),
                    Ticket = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "banishments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(type: "integer", nullable: true),
                    IpAddress = table.Column<IPAddress>(type: "inet", nullable: true),
                    MacAddress = table.Column<PhysicalAddress>(type: "macaddr", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ExpiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banishments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Community = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    VisibleHierarchy = table.Column<int>(type: "integer", nullable: false),
                    JoinableHierarchy = table.Column<int>(type: "integer", nullable: false),
                    IpAddress = table.Column<IPAddress>(type: "inet", nullable: true),
                    Port = table.Column<int>(type: "integer", nullable: true),
                    OpenedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "accounts_activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    IpAddress = table.Column<IPAddress>(type: "inet", nullable: false),
                    ConnectedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts_activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_accounts_activities_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accounts_characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServerId = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts_characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_accounts_characters_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accounts_relations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromAccountId = table.Column<int>(type: "integer", nullable: false),
                    ToAccountId = table.Column<int>(type: "integer", nullable: false),
                    RelationType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts_relations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_accounts_relations_accounts_FromAccountId",
                        column: x => x.FromAccountId,
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "Id", "CreatedAt", "Hierarchy", "IpAddress", "MacAddress", "Nickname", "Password", "SecretAnswer", "SecretQuestion", "SubscriptionExpiredAt", "SubscriptionStartedAt", "Ticket", "Username" },
                values: new object[] { 1, new DateTime(2025, 6, 1, 13, 24, 16, 8, DateTimeKind.Utc), 40, null, null, null, "21232f297a57a5a743894a0e4a801fc3", "blue", "What is your favorite color?", null, null, null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_Nickname",
                table: "accounts",
                column: "Nickname",
                unique: true)
                .Annotation("Relational:Collation", new[] { "case_insensitive" });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_Ticket",
                table: "accounts",
                column: "Ticket",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accounts_Username",
                table: "accounts",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accounts_activities_AccountId",
                table: "accounts_activities",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accounts_characters_AccountId",
                table: "accounts_characters",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_characters_ServerId",
                table: "accounts_characters",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_relations_FromAccountId",
                table: "accounts_relations",
                column: "FromAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts_activities");

            migrationBuilder.DropTable(
                name: "accounts_characters");

            migrationBuilder.DropTable(
                name: "accounts_relations");

            migrationBuilder.DropTable(
                name: "banishments");

            migrationBuilder.DropTable(
                name: "servers");

            migrationBuilder.DropTable(
                name: "accounts");
        }
    }
}
