// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Krosmoz.Tools.Protocol.Models;

namespace Krosmoz.Tools.Protocol.Storages.Symbols;

/// <summary>
/// Represents a storage for managing and retrieving class symbols.
/// </summary>
public sealed class SymbolStorage : ISymbolStorage
{
    /// <summary>
    /// A thread-safe dictionary to store class symbols, keyed by their names.
    /// </summary>
    private readonly ConcurrentDictionary<string, ClassSymbol> _symbols;

    /// <summary>
    /// Initializes a new instance of the <see cref="SymbolStorage"/> class.
    /// </summary>
    public SymbolStorage()
    {
        _symbols = [];
    }

    /// <summary>
    /// Adds a class symbol to the storage.
    /// </summary>
    /// <param name="symbol">The class symbol to add.</param>
    public void AddSymbol(ClassSymbol symbol)
    {
        _symbols[symbol.Metadata.Name] = symbol;
    }

    /// <summary>
    /// Attempts to retrieve a class symbol by its name.
    /// </summary>
    /// <param name="name">The name of the class symbol to retrieve.</param>
    /// <param name="symbol">
    /// When this method returns, contains the class symbol associated with the specified name,
    /// if the name is found; otherwise, <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if the symbol was found; otherwise, <c>false</c>.</returns>
    public bool TryGetSymbol(string name, [NotNullWhen(true)] out ClassSymbol? symbol)
    {
        return _symbols.TryGetValue(name, out symbol);
    }

    /// <summary>
    /// Retrieves all class symbols stored in the storage.
    /// </summary>
    /// <returns>An enumerable collection of all class symbols.</returns>
    public IEnumerable<ClassSymbol> GetSymbols()
    {
        return _symbols.Values;
    }
}
