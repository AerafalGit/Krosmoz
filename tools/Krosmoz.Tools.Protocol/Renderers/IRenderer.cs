// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Tools.Protocol.Renderers;

/// <summary>
/// Defines a generic interface for rendering symbols of a specific type into a string representation.
/// </summary>
/// <typeparam name="TSymbol">The type of symbol to render, constrained to reference types.</typeparam>
public interface IRenderer<in TSymbol>
    where TSymbol : class
{
    /// <summary>
    /// Renders the provided symbol into its string representation.
    /// </summary>
    /// <param name="symbol">The symbol to render.</param>
    /// <returns>A string representation of the rendered symbol.</returns>
    string Render(TSymbol symbol);
}
