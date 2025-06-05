// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Serialization.ELE.Types;

/// <summary>
/// Represents graphical element data for bounding boxes in the serialization format.
/// </summary>
public sealed class BoundingBoxGraphicalElementData : NormalGraphicalElementData
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BoundingBoxGraphicalElementData"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the graphical element.</param>
    /// <param name="type">The type of the graphical element.</param>
    public BoundingBoxGraphicalElementData(int id, GraphicalElementTypes type) : base(id, type)
    {
    }
}
