// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Serialization.ELE;

/// <summary>
/// Represents the types of graphical elements.
/// </summary>
public enum GraphicalElementTypes
{
    /// <summary>
    /// A normal graphical element.
    /// </summary>
    Normal = 0,

    /// <summary>
    /// A graphical element with a bounding box.
    /// </summary>
    BoundingBox = 1,

    /// <summary>
    /// An animated graphical element.
    /// </summary>
    Animated = 2,

    /// <summary>
    /// A graphical element representing an entity.
    /// </summary>
    Entity = 3,

    /// <summary>
    /// A graphical element with particle effects.
    /// </summary>
    Particles = 4,

    /// <summary>
    /// A blended graphical element.
    /// </summary>
    Blended = 5
}
