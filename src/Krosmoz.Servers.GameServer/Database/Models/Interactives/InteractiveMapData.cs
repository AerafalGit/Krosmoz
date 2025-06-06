// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using MemoryPack;

namespace Krosmoz.Servers.GameServer.Database.Models.Interactives;

[MemoryPackable]
public sealed partial class InteractiveMapData
{
    public int MapId { get; set; }

    public bool OnMap { get; set; }

    public short CellId { get; set; }
}
