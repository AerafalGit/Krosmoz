// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database.Models.Servers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.AuthServer.Database.Configurations.Servers;

/// <summary>
/// Configures the entity type for <see cref="ServerCharacterRecord"/> in the database.
/// </summary>
public sealed class ServerCharacterConfiguration : IEntityTypeConfiguration<ServerCharacterRecord>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="ServerCharacterRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<ServerCharacterRecord> builder)
    {
        builder.HasKey(static x => x.Id);

        builder
            .Property(static x => x.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(static x => x.ServerId)
            .IsRequired();

        builder
            .Property(static x => x.AccountId)
            .IsRequired();

        builder
            .Property(static x => x.CharacterId)
            .IsRequired();

        builder.HasIndex(static x => new { x.ServerId, x.AccountId });

        builder.ToTable("servers_characters");
    }
}
