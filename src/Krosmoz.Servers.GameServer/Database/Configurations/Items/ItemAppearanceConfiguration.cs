// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Database.Models.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.GameServer.Database.Configurations.Items;

/// <summary>
/// Configures the database schema for the <see cref="ItemAppearanceRecord"/> entity.
/// </summary>
public sealed class ItemAppearanceConfiguration : IEntityTypeConfiguration<ItemAppearanceRecord>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="ItemAppearanceRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<ItemAppearanceRecord> builder)
    {
        builder.HasKey(static x => x.Id);

        builder
            .Property(static x => x.Id)
            .ValueGeneratedNever();

        builder
            .Property(static x => x.AppearanceId)
            .IsRequired();

        builder.ToTable("items_appearances");
    }
}
