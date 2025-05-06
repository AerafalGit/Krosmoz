// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using MemoryPack;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krosmoz.Core.Extensions;

/// <summary>
/// Provides extension methods for configuring property builders in Entity Framework Core.
/// </summary>
public static class PropertyBuilderExtensions
{
    /// <summary>
    /// Configures a property to use blob conversion for serialization and deserialization.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property, which must implement <see cref="IMemoryPackable{T}"/>.</typeparam>
    /// <param name="builder">The property builder to configure.</param>
    /// <returns>The configured property builder.</returns>
    public static PropertyBuilder<TProperty> HasBlobConversion<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TProperty>(this PropertyBuilder<TProperty> builder)
        where TProperty : IMemoryPackable<TProperty>
    {
        return builder.HasConversion(
            static property => Serialize(property),
            static property => Deserialize<TProperty>(property),
            new ValueComparer<TProperty>(
                static (x, y) => x!.Equals(y),
                static property => property.GetHashCode(),
                static property => property)
        );
    }

    /// <summary>
    /// Serializes a property to a byte array using MemoryPack.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property, which must implement <see cref="IMemoryPackable{T}"/>.</typeparam>
    /// <param name="property">The property to serialize.</param>
    /// <returns>A byte array representing the serialized property.</returns>
    /// <exception cref="MemoryPackSerializationException">Thrown if serialization fails.</exception>
    private static byte[] Serialize<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TProperty>(TProperty property)
        where TProperty : IMemoryPackable<TProperty>
    {
        return MemoryPackSerializer.Serialize(property);
    }

    /// <summary>
    /// Deserializes a byte array to a property using MemoryPack.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property, which must implement <see cref="IMemoryPackable{T}"/>.</typeparam>
    /// <param name="data">The byte array to deserialize.</param>
    /// <returns>The deserialized property.</returns>
    /// <exception cref="MemoryPackSerializationException">Thrown if deserialization fails.</exception>
    private static TProperty Deserialize<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TProperty>(byte[] data)
        where TProperty : IMemoryPackable<TProperty>
    {
        return MemoryPackSerializer.Deserialize<TProperty>(data) ??
               throw new MemoryPackSerializationException($"Failed to deserialize {typeof(TProperty).Name}.");
    }
}
