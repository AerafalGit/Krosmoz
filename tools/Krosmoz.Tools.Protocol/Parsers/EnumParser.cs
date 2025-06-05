// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Text.RegularExpressions;
using Krosmoz.Tools.Protocol.Models;
using Krosmoz.Tools.Protocol.Storages.Expressions;

namespace Krosmoz.Tools.Protocol.Parsers;

/// <summary>
/// Parses symbol metadata into an <see cref="EnumSymbol"/> representation.
/// </summary>
public sealed class EnumParser : IParser<EnumSymbol>
{
    /// <summary>
    /// Parses the provided symbol metadata into an <see cref="EnumSymbol"/>.
    /// </summary>
    /// <param name="symbolMetadata">The metadata of the symbol to parse.</param>
    /// <returns>An <see cref="EnumSymbol"/> representing the parsed symbol.</returns>
    /// <exception cref="Exception">Thrown if required groups are not found in the regex match.</exception>
    public EnumSymbol Parse(SymbolMetadata symbolMetadata)
    {
        var enumSymbol = new EnumSymbol { Metadata = symbolMetadata, Properties = [] };

        foreach (var match in RegexStorage.EnumProperty().Matches(symbolMetadata.Source).Cast<Match>())
        {
            if (!match.Groups.TryGetValue("name", out var nameGroup))
                throw new Exception("Enum property name group not found.");

            if (!match.Groups.TryGetValue("type", out var typeGroup))
                throw new Exception("Enum property type group not found.");

            if (!match.Groups.TryGetValue("value", out var valueGroup))
                throw new Exception("Enum property value group not found.");

            var name = nameGroup.Value;
            var type = typeGroup.Value;
            var value = ParseValue(valueGroup.Value);

            enumSymbol.Properties.Add(new EnumPropertySymbol
            {
                Name = name,
                Type = type,
                Value = value
            });
        }

        return enumSymbol;
    }

    /// <summary>
    /// Parses a string value into a long integer.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>The parsed long integer value.</returns>
    private static long ParseValue(string value)
    {
        return value.StartsWith("0x")
            ? Convert.ToInt64(value, 16)
            : Convert.ToInt64(value);
    }
}
