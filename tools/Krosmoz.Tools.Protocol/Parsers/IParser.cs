// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Tools.Protocol.Models;

namespace Krosmoz.Tools.Protocol.Parsers;

/// <summary>
/// Defines a generic interface for parsing symbol metadata into a specific type of symbol.
/// </summary>
/// <typeparam name="TSymbol">The type of symbol to parse, constrained to reference types.</typeparam>
public interface IParser<out TSymbol>
    where TSymbol : class
{
    /// <summary>
    /// Parses the provided symbol metadata into an instance of <typeparamref name="TSymbol"/>.
    /// </summary>
    /// <param name="symbolMetadata">The metadata of the symbol to parse.</param>
    /// <returns>An instance of <typeparamref name="TSymbol"/> representing the parsed symbol.</returns>
    TSymbol Parse(SymbolMetadata symbolMetadata);
}
