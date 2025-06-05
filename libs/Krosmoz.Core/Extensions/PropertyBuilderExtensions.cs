// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.Contracts;
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
    /// Configures a property to use Protobuf serialization and deserialization for its value conversion.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The property builder to configure.</param>
    /// <param name="valueComparer">The value comparer to use for the property.</param>
    /// <returns>The configured property builder.</returns>
    public static PropertyBuilder<TProperty> HasBlobConversion<TProperty>(this PropertyBuilder<TProperty> builder, ValueComparer valueComparer)
    {
        return builder.HasConversion(
            static x => Serialize(x),
            static x => Deserialize<TProperty>(x),
            valueComparer);
    }

    /// <summary>
    /// Serializes an object to a byte array using MemoryPack.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <returns>A byte array representing the serialized object.</returns>
    [Pure]
    private static byte[] Serialize<T>(T value)
    {
        return MemoryPackSerializer.Serialize(value);
    }

    /// <summary>
    /// Deserializes a byte array to an object using MemoryPack.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="value">The byte array to deserialize.</param>
    /// <returns>The deserialized object.</returns>
    [Pure]
    private static T Deserialize<T>(byte[] value)
    {
        return MemoryPackSerializer.Deserialize<T>(value)!;
    }
}

