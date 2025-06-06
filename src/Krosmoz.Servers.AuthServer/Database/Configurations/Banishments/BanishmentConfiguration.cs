// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database.Models.Banishments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.AuthServer.Database.Configurations.Banishments;

/// <summary>
/// Configures the database schema for the <see cref="BanishmentRecord"/> entity.
/// </summary>
public sealed class BanishmentConfiguration : IEntityTypeConfiguration<BanishmentRecord>
{
    /// <summary>
    /// Configures the properties and table mapping for the <see cref="BanishmentRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<BanishmentRecord> builder)
    {
        builder.HasKey(static x => x.Id);

        builder
            .Property(static x => x.Id)
            .ValueGeneratedOnAdd();

        builder.ToTable("banishments");
    }
}
