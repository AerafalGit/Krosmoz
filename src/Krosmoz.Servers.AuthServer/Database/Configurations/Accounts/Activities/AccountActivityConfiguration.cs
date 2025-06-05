// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.AuthServer.Database.Models.Accounts.Activities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.AuthServer.Database.Configurations.Accounts.Activities;

/// <summary>
/// Configures the database schema for the <see cref="AccountActivityRecord"/> entity.
/// </summary>
public sealed class AccountActivityConfiguration : IEntityTypeConfiguration<AccountActivityRecord>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="AccountActivityRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<AccountActivityRecord> builder)
    {
        builder.HasKey(static x => x.Id);

        builder
            .Property(static x => x.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(static x => x.AccountId)
            .IsRequired();

        builder
            .Property(static x => x.IpAddress)
            .IsRequired();

        builder
            .Property(static x => x.ConnectedAt)
            .IsRequired();

        builder.ToTable("accounts_activities");
    }
}
