// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Tools.Protocol.Models;

namespace Krosmoz.Tools.Protocol.Storages.Symbols;

/// <summary>
/// Represents a storage interface for managing class symbols.
/// </summary>
public interface ISymbolStorage
{
    /// <summary>
    /// Adds a class symbol to the storage.
    /// </summary>
    /// <param name="symbol">The class symbol to add.</param>
    void AddSymbol(ClassSymbol symbol);

    /// <summary>
    /// Attempts to retrieve a class symbol by its name.
    /// </summary>
    /// <param name="name">The name of the class symbol to retrieve.</param>
    /// <param name="symbol">
    /// When this method returns, contains the class symbol associated with the specified name,
    /// if the name is found; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if a class symbol with the specified name is found; otherwise, <c>false</c>.
    /// </returns>
    bool TryGetSymbol(string name, [NotNullWhen(true)] out ClassSymbol? symbol);

    /// <summary>
    /// Retrieves all class symbols stored in the storage.
    /// </summary>
    /// <returns>An enumerable collection of all class symbols.</returns>
    IEnumerable<ClassSymbol> GetSymbols();
}
