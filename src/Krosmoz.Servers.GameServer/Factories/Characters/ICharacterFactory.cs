// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Breeds;
using Krosmoz.Protocol.Ipc.Types.Accounts;
using Krosmoz.Servers.GameServer.Database.Models.Characters;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Models.Appearances;
using Krosmoz.Servers.GameServer.Models.Heroes;
using Krosmoz.Servers.GameServer.Network.Transport;

namespace Krosmoz.Servers.GameServer.Factories.Characters;

public interface ICharacterFactory
{
    CharacterRecord CreateCharacterRecord(IpcAccount account, string name, Breed breed, Head head, bool sex, ActorLook look);

    CharacterActor CreateCharacter(DofusConnection connection, CharacterRecord characterRecord);
}
