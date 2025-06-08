using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Krosmoz.Servers.AuthServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class CaseInsensitiveCollation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM pg_collation
                        WHERE collname = 'case_insensitive'
                    ) THEN
                        CREATE COLLATION case_insensitive (
                            LOCALE = 'en-u-ks-primary',
                            PROVIDER = icu,
                            DETERMINISTIC = False
                        );
                    END IF;
                END$$;
            ");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP COLLATION IF EXISTS case_insensitive;");
        }
    }
}
