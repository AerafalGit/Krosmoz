// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Database.Models.Items;

public sealed class ItemAppearanceRecord
{
    public int Id { get; init; }

    public int AppearanceId { get; init; }

    public string? CustomLook { get; init; }
}
