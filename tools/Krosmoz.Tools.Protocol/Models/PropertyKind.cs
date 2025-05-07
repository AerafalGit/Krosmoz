// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Tools.Protocol.Models;

/// <summary>
/// Represents the kind of a property.
/// </summary>
public enum PropertyKind
{
    /// <summary>
    /// Represents an object property.
    /// </summary>
    Object,

    /// <summary>
    /// Represents a primitive property.
    /// </summary>
    Primitive,

    /// <summary>
    /// Represents a vector property.
    /// </summary>
    Vector
}
