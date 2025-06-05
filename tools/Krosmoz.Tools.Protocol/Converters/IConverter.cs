// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Tools.Protocol.Converters;

/// <summary>
/// Defines a generic interface for converting symbols of a specific type.
/// </summary>
/// <typeparam name="TSymbol">The type of symbol to convert, constrained to reference types.</typeparam>
public interface IConverter<in TSymbol>
    where TSymbol : class
{
    /// <summary>
    /// Converts the provided symbol into a desired format or representation.
    /// </summary>
    /// <param name="symbol">The symbol to convert.</param>
    void Convert(TSymbol symbol);
}
