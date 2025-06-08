// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Breeds;
using Krosmoz.Protocol.Ipc.Types.Accounts;
using Krosmoz.Servers.GameServer.Database.Models.Characters;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Models.Appearances;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Factories.Characters;

/// <summary>
/// Interface for a factory responsible for creating and initializing character-related objects for the game server.
/// </summary>
public interface ICharacterFactory
{
    /// <summary>
    /// Creates a new character record with the specified parameters.
    /// </summary>
    /// <param name="account">The account associated with the character.</param>
    /// <param name="name">The name of the character.</param>
    /// <param name="breed">The breed of the character.</param>
    /// <param name="head">The head appearance of the character.</param>
    /// <param name="sex">The gender of the character.</param>
    /// <param name="look">The visual appearance of the character.</param>
    /// <returns>A new <see cref="CharacterRecord"/> instance.</returns>
    CharacterRecord CreateCharacterRecord(IpcAccount account, string name, Breed breed, Head head, bool sex, ActorLook look);

    /// <summary>
    /// Creates a new character actor from the specified character record and connection.
    /// </summary>
    /// <param name="connection">The connection associated with the character.</param>
    /// <param name="characterRecord">The character record to initialize the actor from.</param>
    /// <returns>A new <see cref="CharacterActor"/> instance.</returns>
    CharacterActor CreateCharacter(DofusConnection connection, CharacterRecord characterRecord);
}
