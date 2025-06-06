// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;

namespace Krosmoz.Servers.GameServer.Models.GameActions;

/// <summary>
/// Represents the base class for game actions.
/// </summary>
public abstract class GameAction
{
    /// <summary>
    /// Gets the type of the game action.
    /// </summary>
    public abstract GameActionTypes Type { get; }
}
