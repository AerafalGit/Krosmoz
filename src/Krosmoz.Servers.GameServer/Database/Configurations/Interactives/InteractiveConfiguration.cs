// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Servers.GameServer.Database.Models.Interactives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.GameServer.Database.Configurations.Interactives;

/// <summary>
/// Configures the database schema for the <see cref="InteractiveRecord"/> entity.
/// </summary>
public sealed class InteractiveConfiguration : IEntityTypeConfiguration<InteractiveRecord>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="InteractiveRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<InteractiveRecord> builder)
    {
        builder.HasKey(static x => x.Id);

        builder
            .Property(static x => x.Id)
            .ValueGeneratedNever();

        builder
            .Property(static x => x.GfxId)
            .IsRequired();

        builder
            .Property(static x => x.Animated)
            .IsRequired();

        builder
            .Property(static x => x.MapId)
            .IsRequired();

        builder
            .Property(static x => x.ElementId)
            .IsRequired();

        builder
            .Property(static x => x.MapsData)
            .HasBlobConversion(new ArrayStructuralComparer<InteractiveMapData>())
            .IsRequired();

        builder
            .HasMany(static x => x.InteractiveActions)
            .WithOne()
            .HasForeignKey(static x => x.InteractiveId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("interactives");
    }
}
