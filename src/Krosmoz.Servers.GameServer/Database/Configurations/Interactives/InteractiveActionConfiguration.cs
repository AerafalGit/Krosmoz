// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Database.Models.Interactives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.GameServer.Database.Configurations.Interactives;

/// <summary>
/// Configures the database schema for the <see cref="InteractiveActionRecord"/> entity.
/// </summary>
public sealed class InteractiveActionConfiguration : IEntityTypeConfiguration<InteractiveActionRecord>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="InteractiveActionRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<InteractiveActionRecord> builder)
    {
        builder.HasKey(static x => x.Id);

        builder
            .Property(static x => x.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(static x => x.InteractiveId)
            .IsRequired();

        builder
            .Property(static x => x.InteractiveTemplateId)
            .IsRequired();

        builder
            .Property(static x => x.SkillId)
            .IsRequired();

        builder
            .Property(static x => x.Type)
            .IsRequired();

        builder.Ignore(static x => x.Action);

        builder.ToTable("interactives_actions");
    }
}
