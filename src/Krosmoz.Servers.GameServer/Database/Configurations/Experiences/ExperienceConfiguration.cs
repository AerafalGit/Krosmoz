// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Database.Models.Experiences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.GameServer.Database.Configurations.Experiences;

/// <summary>
/// Configures the database schema for the <see cref="ExperienceRecord"/> entity.
/// </summary>
public sealed class ExperienceConfiguration : IEntityTypeConfiguration<ExperienceRecord>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="ExperienceRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<ExperienceRecord> builder)
    {
        builder.HasKey(static x => x.Level);

        builder
            .Property(static x => x.Level)
            .ValueGeneratedNever();

        builder
            .Property(static x => x.CharacterXp)
            .IsRequired();

        builder
            .Property(static x => x.GuildXp)
            .IsRequired();

        builder.ToTable("experiences");
    }
}
