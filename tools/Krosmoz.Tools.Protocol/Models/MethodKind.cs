// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Tools.Protocol.Models;

/// <summary>
/// Represents the kinds of methods.
/// </summary>
public enum MethodKind
{
    /// <summary>
    /// Represents a primitive method.
    /// </summary>
    Primitive,

    /// <summary>
    /// Represents a method that operates on a vector of primitives.
    /// </summary>
    VectorPrimitive,

    /// <summary>
    /// Represents a method used for serialization or deserialization.
    /// </summary>
    SerializeOrDeserialize,

    /// <summary>
    /// Represents a method managed by the ProtocolTypeManager.
    /// </summary>
    ProtocolTypeManager,

    /// <summary>
    /// Represents a method that uses the BooleanByteWrapper utility.
    /// </summary>
    BooleanByteWrapper
}
