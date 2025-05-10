// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Servers.AuthServer.Database.Configurations.Accounts;

/// <summary>
/// Configures the entity type for <see cref="AccountRecord"/> in the database.
/// </summary>
public sealed class AccountConfiguration : IEntityTypeConfiguration<AccountRecord>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="AccountRecord"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<AccountRecord> builder)
    {
        builder.HasKey(static x => x.Id);

        builder
            .Property(static x => x.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(static x => x.Username)
            .IsRequired();

        builder
            .Property(static x => x.Password)
            .IsRequired();

        builder
            .Property(static x => x.Language)
            .HasEnumToStringConversion()
            .IsRequired();

        builder
            .Property(static x => x.Hierarchy)
            .HasEnumToStringConversion()
            .IsRequired();

        builder
            .Property(static x => x.SecretQuestion)
            .IsRequired();

        builder
            .Property(static x => x.SecretAnswer)
            .IsRequired();

        builder
            .Property(static x => x.CreatedAt)
            .IsRequired();

        builder
            .Property(static x => x.UpdatedAt)
            .IsRequired();

        builder
            .HasMany(static x => x.Characters)
            .WithOne()
            .HasForeignKey(static x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasIndex(static x => new { x.Username, x.Nickname })
            .IsUnique();

        builder.ToTable("accounts");
    }
}
