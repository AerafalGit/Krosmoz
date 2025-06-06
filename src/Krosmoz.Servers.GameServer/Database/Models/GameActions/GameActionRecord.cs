// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.GameServer.Models.GameActions;

namespace Krosmoz.Servers.GameServer.Database.Models.GameActions;

public abstract class GameActionRecord
{
    public required GameActionTypes Type { get; set; }

    public string[]? Parameters { get; set; }

    public string? Criterion { get; set; }

    public GameAction? Action { get; set; }
}
