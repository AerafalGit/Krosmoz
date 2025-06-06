// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database.Models.Servers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.AuthServer.Database.Configurations.Servers;

/// <summary>
/// Configures the database schema for the <see cref="ServerRecord"/> entity.
/// </summary>
public sealed class ServerConfiguration : IEntityTypeConfiguration<ServerRecord>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="ServerRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<ServerRecord> builder)
    {
        builder.HasKey(static x => x.Id);

        builder
            .Property(static x => x.Id)
            .ValueGeneratedNever();

        builder
            .Property(static x => x.Name)
            .IsRequired();

        builder
            .Property(static x => x.Type)
            .IsRequired();

        builder
            .Property(static x => x.Community)
            .IsRequired();

        builder
            .Property(x => x.Status)
            .IsRequired();

        builder
            .Property(static x => x.VisibleHierarchy)
            .IsRequired();

        builder
            .Property(static x => x.JoinableHierarchy)
            .IsRequired();

        builder.ToTable("servers");
    }
}
