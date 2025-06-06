// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Ipc.Types.Accounts;

namespace Krosmoz.Servers.GameServer.Models.Heroes;

/// <summary>
/// Represents a collection of heroes.
/// </summary>
public sealed class Heroes
{
    /// <summary>
    /// Gets or sets the account associated with the heroes.
    /// </summary>
    public IpcAccount Account { get; set; } = null!;
}
