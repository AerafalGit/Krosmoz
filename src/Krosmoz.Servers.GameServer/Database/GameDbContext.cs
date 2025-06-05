// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Servers.GameServer.Database;

/// <summary>
/// Represents the database context for the game server,
/// providing access to the database and configuring entity mappings.
/// </summary>
public sealed class GameDbContext : DbContext
{
    /// <summary>
    /// The collation used for case-insensitive string comparisons in the database.
    /// </summary>
    private const string CaseInsensitiveCollation = "case_insensitive";

    /// <summary>
    /// Initializes a new instance of the <see cref="GameDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the entity mappings for the database context by applying configurations
    /// from the assembly containing the <see cref="GameDbContext"/> class.
    /// </summary>
    /// <param name="builder">The <see cref="ModelBuilder"/> used to configure entity mappings.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasCollation(CaseInsensitiveCollation, locale: "en-u-ks-primary", provider: "icu", deterministic: false);
    }
}
