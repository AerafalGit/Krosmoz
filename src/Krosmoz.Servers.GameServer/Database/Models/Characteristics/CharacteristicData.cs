// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using MemoryPack;

namespace Krosmoz.Servers.GameServer.Database.Models.Characteristics;

/// <summary>
/// Represents the data structure for a characteristic.
/// </summary>
[MemoryPackable]
public sealed partial class CharacteristicData
{
    /// <summary>
    /// Gets or sets the base value of the characteristic.
    /// </summary>
    public int Base { get; set; }

    /// <summary>
    /// Gets or sets the bonus value of the characteristic.
    /// </summary>
    public int Bonus { get; set; }
}
