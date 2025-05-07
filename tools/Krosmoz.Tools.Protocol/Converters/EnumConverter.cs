// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Tools.Protocol.Extensions;
using Krosmoz.Tools.Protocol.Models;

namespace Krosmoz.Tools.Protocol.Converters;

/// <summary>
/// Converts enumeration symbols by modifying their metadata and properties.
/// </summary>
public sealed class EnumConverter : IConverter<EnumSymbol>
{
    /// <summary>
    /// Converts the provided enumeration symbol by updating its metadata name
    /// and transforming its properties to adhere to specific naming and type conventions.
    /// </summary>
    /// <param name="symbol">The enumeration symbol to convert.</param>
    public void Convert(EnumSymbol symbol)
    {
        symbol.Metadata.Name = symbol.Metadata.Name
            .Replace("Enum", string.Empty)
            .Pluralize();

        foreach (var propertySymbol in symbol.Properties)
        {
            propertySymbol.Name = propertySymbol.Name.ToPascalCase();

            if (propertySymbol.Type is "Number")
                propertySymbol.Type = "long";
        }
    }
}
