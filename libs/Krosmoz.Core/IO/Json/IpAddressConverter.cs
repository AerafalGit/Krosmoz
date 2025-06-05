// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Krosmoz.Core.IO.Json;

/// <summary>
/// A custom JSON converter for <see cref="IPAddress"/> objects.
/// Provides methods to read and write <see cref="IPAddress"/> instances
/// during JSON serialization and deserialization.
/// </summary>
public sealed class IpAddressConverter : JsonConverter<IPAddress>
{
    /// <summary>
    /// Reads and converts the JSON string representation of an IP address
    /// into an <see cref="IPAddress"/> object.
    /// </summary>
    /// <param name="reader">The <see cref="Utf8JsonReader"/> to read the JSON data from.</param>
    /// <param name="typeToConvert">The type of the object to convert (ignored in this implementation).</param>
    /// <param name="options">Options for JSON serialization (ignored in this implementation).</param>
    /// <returns>
    /// An <see cref="IPAddress"/> object if the JSON string is a valid IP address;
    /// otherwise, <c>null</c>.
    /// </returns>
    public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is not JsonTokenType.String)
            return null;

        return IPAddress.TryParse(reader.GetString(), out var ipAddress)
            ? ipAddress
            : null;
    }

    /// <summary>
    /// Writes the <see cref="IPAddress"/> object as a JSON string representation.
    /// </summary>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write the JSON data to.</param>
    /// <param name="value">The <see cref="IPAddress"/> object to serialize.</param>
    /// <param name="options">Options for JSON serialization (ignored in this implementation).</param>
    public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
