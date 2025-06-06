// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Database.Configurations.Experiences;
using Krosmoz.Servers.GameServer.Database.Configurations.Interactives;
using Krosmoz.Servers.GameServer.Database.Configurations.Items;
using Krosmoz.Servers.GameServer.Database.Configurations.Maps;
using Krosmoz.Servers.GameServer.Database.Models.Experiences;
using Krosmoz.Servers.GameServer.Database.Models.Interactives;
using Krosmoz.Servers.GameServer.Database.Models.Items;
using Krosmoz.Servers.GameServer.Database.Models.Maps;
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
    /// Gets or sets the DbSet representing the collection of <see cref="ExperienceRecord"/> entities
    /// in the database. This property is required and provides access to the "Experiences" table.
    /// </summary>
    public required DbSet<ExperienceRecord> Experiences { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing the collection of <see cref="InteractiveRecord"/> entities
    /// in the database. This property is required and provides access to the "Interactives" table.
    /// </summary>
    public required DbSet<InteractiveRecord> Interactives { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing the collection of <see cref="InteractiveActionRecord"/> entities
    /// in the database. This property is required and provides access to the "InteractiveActions" table.
    /// </summary>
    public required DbSet<InteractiveActionRecord> InteractiveActions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing the collection of <see cref="MapRecord"/> entities
    /// in the database. This property is required and provides access to the "Maps" table.
    /// </summary>
    public required DbSet<MapRecord> Maps { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing the collection of <see cref="ItemAppearanceRecord"/> entities
    /// in the database. This property is required and provides access to the "ItemAppearances" table.
    /// </summary>
    public required DbSet<ItemAppearanceRecord> ItemAppearances { get; set; }

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

        builder
            .ApplyConfiguration(new ExperienceConfiguration())
            .ApplyConfiguration(new InteractiveConfiguration())
            .ApplyConfiguration(new InteractiveActionConfiguration())
            .ApplyConfiguration(new MapConfiguration())
            .ApplyConfiguration(new ItemAppearanceConfiguration());
    }
}
