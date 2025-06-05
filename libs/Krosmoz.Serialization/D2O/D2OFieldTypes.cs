// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Serialization.D2O;

/// <summary>
/// Represents the types of fields that can be used in a D2O file.
/// </summary>
public enum D2OFieldTypes
{
    /// <summary>
    /// Represents an integer field type.
    /// </summary>
    Int = -1,

    /// <summary>
    /// Represents a boolean field type.
    /// </summary>
    Boolean = -2,

    /// <summary>
    /// Represents a string field type.
    /// </summary>
    String = -3,

    /// <summary>
    /// Represents a number field type.
    /// </summary>
    Number = -4,

    /// <summary>
    /// Represents an internationalized text field type.
    /// </summary>
    I18N = -5,

    /// <summary>
    /// Represents an unsigned integer field type.
    /// </summary>
    UInt = -6,

    /// <summary>
    /// Represents a vector field type.
    /// </summary>
    Vector = -99
}
