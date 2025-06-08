// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database.Configurations.Accounts;
using Krosmoz.Servers.AuthServer.Database.Configurations.Accounts.Activities;
using Krosmoz.Servers.AuthServer.Database.Configurations.Accounts.Characters;
using Krosmoz.Servers.AuthServer.Database.Configurations.Accounts.Relations;
using Krosmoz.Servers.AuthServer.Database.Configurations.Banishments;
using Krosmoz.Servers.AuthServer.Database.Configurations.Servers;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts.Activities;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts.Characters;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts.Relations;
using Krosmoz.Servers.AuthServer.Database.Models.Banishments;
using Krosmoz.Servers.AuthServer.Database.Models.Servers;
using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Servers.AuthServer.Database;

/// <summary>
/// Represents the database context for the authentication server,
/// providing access to the database and configuring entity mappings.
/// </summary>
public sealed class AuthDbContext : DbContext
{
    /// <summary>
    /// The collation used for case-insensitive string comparisons in the database.
    /// </summary>
    internal const string CaseInsensitiveCollation = "case_insensitive";

    /// <summary>
    /// Gets or sets the DbSet representing the collection of <see cref="AccountRecord"/> entities
    /// in the database. This property is required and provides access to the "accounts" table.
    /// </summary>
    public required DbSet<AccountRecord> Accounts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing the collection of <see cref="AccountCharacterRecord"/> entities
    /// in the database. This property is required and provides access to the "accounts_characters" table.
    /// </summary>
    public required DbSet<AccountCharacterRecord> AccountCharacters { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing the collection of <see cref="AccountRelationRecord"/> entities
    /// in the database. This property is required and provides access to the "accounts_relations" table.
    /// </summary>
    public required DbSet<AccountRelationRecord> AccountRelations { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing the collection of <see cref="AccountActivityRecord"/> entities
    /// in the database. This property is required and provides access to the "accounts_activities" table.
    /// </summary>
    public required DbSet<AccountActivityRecord> AccountActivities { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing the collection of <see cref="BanishmentRecord"/> entities
    /// in the database. This property is required and provides access to the "Banishments" table.
    /// </summary>
    public required DbSet<BanishmentRecord> Banishments { get; set; }

    /// <summary>
    /// Gets or sets the DbSet representing the collection of <see cref="ServerRecord"/> entities
    /// in the database. This property is required and provides access to the "Servers" table.
    /// </summary>
    public required DbSet<ServerRecord> Servers { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the entity mappings for the database context by applying configurations
    /// from the assembly containing the <see cref="AuthDbContext"/> class.
    /// </summary>
    /// <param name="builder">The <see cref="ModelBuilder"/> used to configure entity mappings.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasCollation("case_insensitive", locale: "en-u-ks-primary", provider: "icu", deterministic: false);

        builder
            .ApplyConfiguration(new AccountConfiguration())
            .ApplyConfiguration(new AccountCharacterConfiguration())
            .ApplyConfiguration(new AccountRelationConfiguration())
            .ApplyConfiguration(new AccountActivityConfiguration())
            .ApplyConfiguration(new BanishmentConfiguration())
            .ApplyConfiguration(new ServerConfiguration());
    }
}
