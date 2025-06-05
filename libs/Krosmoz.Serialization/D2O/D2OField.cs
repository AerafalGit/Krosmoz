// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;

namespace Krosmoz.Serialization.D2O;

/// <summary>
/// Represents a field in a D2O file.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class D2OField
{
    /// <summary>
    /// Gets the name of the field.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the type of the field.
    /// </summary>
    public int Type { get; }

    /// <summary>
    /// Gets the list of inner type names, if applicable.
    /// </summary>
    public List<string> InnerTypeNames { get; }

    /// <summary>
    /// Gets the list of inner type identifiers, if applicable.
    /// </summary>
    public List<int> InnerTypeIds { get; }

    /// <summary>
    /// Determines whether the field is queryable based on its type and inner type identifiers.
    /// </summary>
    /// <remarks>
    /// A field is considered queryable if:
    /// - It is of type <see cref="D2OFieldTypes.Vector"/> and has at least one valid inner type identifier.
    /// - Its type is greater than 0.
    /// </remarks>
    public bool IsQueryable =>
        (Type is (int)D2OFieldTypes.Vector && InnerTypeIds.Count > 0 && InnerTypeIds[0] > 0) || Type > 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="D2OField"/> class.
    /// </summary>
    /// <param name="name">The name of the field.</param>
    /// <param name="type">The type of the field.</param>
    /// <param name="innerTypeNames">The list of inner type names.</param>
    /// <param name="innerTypeIds">The list of inner type identifiers.</param>
    public D2OField(string name, int type, List<string> innerTypeNames, List<int> innerTypeIds)
    {
        Name = name;
        Type = type;
        InnerTypeNames = innerTypeNames;
        InnerTypeIds = innerTypeIds;
    }

    /// <summary>
    /// Returns a string representation of the D2O field.
    /// </summary>
    /// <returns>A string containing the field's name, type, and optional inner type details.</returns>
    public override string ToString()
    {
        return InnerTypeNames is not null && InnerTypeIds is not null
            ? $"Name: {Name}, Type: {(Type > 0 ? (D2OFieldTypes)Type : Type)}, InnerTypeNames: {InnerTypeNames.Count}, InnerTypeIds: {InnerTypeIds.Count}"
            : $"Name: {Name}, Type: {(Type > 0 ? (D2OFieldTypes)Type : Type)}";
    }
}
