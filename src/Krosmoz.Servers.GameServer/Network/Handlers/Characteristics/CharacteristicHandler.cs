// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher.Attributes;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Messages.Game.Context.Roleplay.Stats;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Characteristics;

namespace Krosmoz.Servers.GameServer.Network.Handlers.Characteristics;

/// <summary>
/// Handles operations related to character characteristics.
/// </summary>
public sealed class CharacteristicHandler
{
    private readonly ICharacteristicService _characteristicService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacteristicHandler"/> class.
    /// </summary>
    /// <param name="characteristicService">The service responsible for managing character characteristics.</param>
    public CharacteristicHandler(ICharacteristicService characteristicService)
    {
        _characteristicService = characteristicService;
    }

    /// <summary>
    /// Handles the request to upgrade a character's stats asynchronously.
    /// </summary>
    /// <param name="connection">The connection associated with the player making the request.</param>
    /// <param name="message">The message containing the stat upgrade request details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [MessageHandler]
    public Task HandleStatsUpgradeRequestAsync(DofusConnection connection, StatsUpgradeRequestMessage message)
    {
        return _characteristicService.UpgradeCharacteristicAsync(
            connection.Heroes.Master,
            (CharacteristicIds)message.StatId,
            message.BoostPoint
        );
    }
}
