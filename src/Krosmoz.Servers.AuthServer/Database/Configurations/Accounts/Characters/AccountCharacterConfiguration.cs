// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database.Models.Accounts.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.AuthServer.Database.Configurations.Accounts.Characters;

/// <summary>
/// Configures the database schema for the <see cref="AccountCharacterRecord"/> entity.
/// </summary>
public sealed class AccountCharacterConfiguration : IEntityTypeConfiguration<AccountCharacterRecord>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="AccountCharacterRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<AccountCharacterRecord> builder)
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

        builder.HasIndex(static x => x.ServerId);
        builder.HasIndex(static x => x.AccountId);

        builder.ToTable("accounts_characters");
    }
}
