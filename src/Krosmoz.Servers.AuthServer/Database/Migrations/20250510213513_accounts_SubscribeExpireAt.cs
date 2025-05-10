using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Krosmoz.Servers.AuthServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class accounts_SubscribeExpireAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionExpireAt",
                table: "accounts",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionExpireAt",
                table: "accounts");
        }
    }
}
