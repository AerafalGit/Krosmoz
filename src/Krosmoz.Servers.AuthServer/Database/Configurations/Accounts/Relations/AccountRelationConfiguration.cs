// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database.Models.Accounts.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.AuthServer.Database.Configurations.Accounts.Relations;

/// <summary>
/// Configures the database schema for the <see cref="AccountRelationRecord"/> entity.
/// </summary>
public sealed class AccountRelationConfiguration : IEntityTypeConfiguration<AccountRelationRecord>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="AccountRelationRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<AccountRelationRecord> builder)
    {
        builder.HasKey(static x => x.Id);

        builder
            .Property(static x => x.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(static x => x.FromAccountId)
            .IsRequired();

        builder
            .Property(static x => x.ToAccountId)
            .IsRequired();

        builder
            .Property(static x => x.RelationType)
            .IsRequired();

        builder
            .Property(static x => x.CreatedAt)
            .IsRequired();

        builder.ToTable("accounts_relations");
    }
}
