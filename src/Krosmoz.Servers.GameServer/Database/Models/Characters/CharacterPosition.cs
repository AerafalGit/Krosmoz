// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Runtime.InteropServices;

namespace Krosmoz.Servers.GameServer.Database.Models.Characters;

[StructLayout(LayoutKind.Explicit)]
public struct CharacterPosition
{
    [FieldOffset(0)]
    public int MapId;

    [FieldOffset(4)]
    public short CellId;

    [FieldOffset(6)]
    public sbyte Orientation;

    [FieldOffset(0)]
    public ulong Position;

    public CharacterPosition(int mapId, short cellId, sbyte orientation)
    {
        MapId = mapId;
        CellId = cellId;
        Orientation = orientation;
    }

    public CharacterPosition(ulong position)
    {
        Position = position;
    }
}
