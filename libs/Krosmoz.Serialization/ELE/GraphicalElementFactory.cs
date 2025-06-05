// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Serialization.ELE.Types;

namespace Krosmoz.Serialization.ELE;

/// <summary>
/// Provides a factory for creating instances of graphical element data based on their type.
/// </summary>
public static class GraphicalElementFactory
{
    /// <summary>
    /// Creates an instance of a graphical element data object based on the specified element ID and type.
    /// </summary>
    /// <param name="elementId">The unique identifier of the graphical element.</param>
    /// <param name="type">The type of the graphical element.</param>
    /// <returns>An instance of <see cref="GraphicalElementData"/> corresponding to the specified type.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified type is not recognized.</exception>
    public static GraphicalElementData Create(int elementId, GraphicalElementTypes type)
    {
        return type switch
        {
            GraphicalElementTypes.Normal => new NormalGraphicalElementData(elementId, type),
            GraphicalElementTypes.BoundingBox => new BoundingBoxGraphicalElementData(elementId, type),
            GraphicalElementTypes.Animated => new AnimatedGraphicalElementData(elementId, type),
            GraphicalElementTypes.Entity => new EntityGraphicalElementData(elementId, type),
            GraphicalElementTypes.Particles => new ParticlesGraphicalElementData(elementId, type),
            GraphicalElementTypes.Blended => new BlendedGraphicalElementData(elementId, type),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}
