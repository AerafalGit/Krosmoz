// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Servers.GameServer.Database.Models.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.GameServer.Database.Configurations.Maps;

/// <summary>
/// Configures the database schema for the <see cref="MapRecord"/> entity.
/// </summary>
public sealed class MapConfiguration : IEntityTypeConfiguration<MapRecord>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="MapRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<MapRecord> builder)
    {
        builder.HasKey(static x => x.Id);

        builder
            .Property(static x => x.Id)
            .ValueGeneratedNever();

        builder
            .Property(static x => x.X)
            .IsRequired();

        builder
            .Property(static x => x.Y)
            .IsRequired();

        builder
            .Property(static x => x.Outdoor)
            .IsRequired();

        builder
            .Property(static x => x.Capabilities)
            .IsRequired();

        builder
            .Property(static x => x.SubAreaId)
            .IsRequired();

        builder
            .Property(static x => x.WorldMap)
            .IsRequired();

        builder
            .Property(static x => x.HasPriorityOnWorldMap)
            .IsRequired();

        builder
            .Property(static x => x.PrismAllowed)
            .IsRequired();

        builder
            .Property(static x => x.PvpDisabled)
            .IsRequired();

        builder
            .Property(static x => x.PlacementGenDisabled)
            .IsRequired();

        builder
            .Property(static x => x.MerchantsMax)
            .IsRequired();

        builder
            .Property(static x => x.SpawnDisabled)
            .IsRequired();

        builder
            .Property(static x => x.RedCells)
            .IsRequired();

        builder
            .Property(static x => x.BlueCells)
            .IsRequired();

        builder
            .Property(static x => x.Cells)
            .HasBlobConversion(new ArrayStructuralComparer<CellData>())
            .IsRequired();

        builder
            .HasMany(static x => x.Interactives)
            .WithOne()
            .HasForeignKey(static x => x.MapId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(static x => x.TopNeighbourId)
            .IsRequired();

        builder
            .Property(static x => x.BottomNeighbourId)
            .IsRequired();

        builder
            .Property(static x => x.LeftNeighbourId)
            .IsRequired();

        builder
            .Property(static x => x.RightNeighbourId)
            .IsRequired();

        builder.ToTable("maps");
    }
}
