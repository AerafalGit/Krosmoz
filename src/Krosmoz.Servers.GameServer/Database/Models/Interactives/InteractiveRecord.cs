// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Database.Models.Interactives;

public sealed class InteractiveRecord
{
    public required int Id { get; init; }

    public required int GfxId { get; init; }

    public required bool Animated { get; init; }

    public required int MapId { get; init; }

    public int ElementId { get; init; }

    public required InteractiveMapData[] MapsData { get; init; }

    public required ICollection<InteractiveActionRecord> InteractiveActions { get; init; }
}
