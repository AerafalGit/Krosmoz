// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Ipc.Types.Accounts;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Models.Heroes;

/// <summary>
/// Represents a collection of heroes associated with an account.
/// </summary>
public sealed class Heroes
{
    /// <summary>
    /// Gets or sets the account associated with the heroes.
    /// </summary>
    public IpcAccount Account { get; set; } = null!;

    /// <summary>
    /// Gets or sets the master hero in the collection.
    /// </summary>
    public CharacterActor Master { get; set; } = null!;

    /// <summary>
    /// Gets the list of slave heroes in the collection.
    /// </summary>
    public List<CharacterActor> Slaves { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Heroes"/> class.
    /// </summary>
    public Heroes()
    {
        Slaves = [];
    }
}
