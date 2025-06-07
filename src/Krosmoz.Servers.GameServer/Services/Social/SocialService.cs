// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Ipc;
using Krosmoz.Protocol.Ipc.Messages.Accounts;
using Krosmoz.Protocol.Messages.Game.Friend;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Models.Social;
using Krosmoz.Servers.GameServer.Services.Servers;
using Krosmoz.Servers.GameServer.Services.World;
using NATS.Client.Core;

namespace Krosmoz.Servers.GameServer.Services.Social;

/// <summary>
/// Provides services for managing social-related operations for characters,
/// including loading relations, sending friend and ignored lists, and spouse information.
/// </summary>
public sealed class SocialService : ISocialService
{
    private readonly INatsConnection _natsConnection;
    private readonly IWorldService _worldService;
    private readonly IServerService _serverService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SocialService"/> class.
    /// </summary>
    /// <param name="natsConnection">The NATS connection used for inter-process communication.</param>
    /// <param name="worldService">The world service for managing character data in the game world.</param>
    /// <param name="serverService">The server service for managing server-specific operations.</param>
    public SocialService(INatsConnection natsConnection, IWorldService worldService, IServerService serverService)
    {
        _natsConnection = natsConnection;
        _worldService = worldService;
        _serverService = serverService;
    }

    /// <summary>
    /// Loads the social relations (e.g., friends, ignored players) of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose relations will be loaded.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task LoadRelationsAsync(CharacterActor character)
    {
        foreach (var relation in character.Account.Relations)
        {
            var accountByIdRequest = new IpcAccountByIdRequest { AccountId = relation.ToAccountId };

            var accountByIdResponse = await _natsConnection.RequestAsync<IpcAccountByIdRequest, IpcAccountByIdResponse>(
                IpcSubjects.AccountById,
                accountByIdRequest,
                cancellationToken: character.Connection.ConnectionClosed);

            if (accountByIdResponse.Data?.Account is null)
                continue;

            var socialAccount = accountByIdResponse.Data.Account;

            foreach (var accountCharacter in socialAccount.Characters)
            {
                if (accountCharacter.ServerId != _serverService.ServerId)
                    continue;

                var socialCharacter = _worldService.FirstOrDefault(x => x.Id == accountCharacter.CharacterId);

                switch (relation.RelationType)
                {
                    case SocialRelationTypeIds.Friend:
                        character.Friends.Add(new SocialWrapper(socialAccount.Id, socialAccount.Nickname, DateTime.UtcNow, socialCharacter));
                        break;

                    case SocialRelationTypeIds.Ignored:
                        character.Ignored.Add(new SocialWrapper(socialAccount.Id, socialAccount.Nickname, DateTime.UtcNow, socialCharacter));
                        break;

                    default:
                        throw new Exception($"Unknown relation type: {relation.RelationType}.");
                }
            }
        }
    }

    /// <summary>
    /// Sends the friend list of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose friend list will be sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask SendFriendListAsync(CharacterActor character)
    {
        return character.Connection.SendAsync(new FriendsListMessage { FriendsList = character.Friends.Select(static friend => friend.GetFriendInformations()) });
    }

    /// <summary>
    /// Sends the ignored list of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose ignored list will be sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask SendIgnoredListAsync(CharacterActor character)
    {
        return character.Connection.SendAsync(new IgnoredListMessage { IgnoredList = character.Ignored.Select(static ignored => ignored.GetIgnoredInformations()) });
    }

    /// <summary>
    /// Sends the spouse information of a character asynchronously.
    /// </summary>
    /// <param name="character">The character actor whose spouse information will be sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask SendSpouseInformationsAsync(CharacterActor character)
    {
        // TODO: Spouse
        return ValueTask.CompletedTask;
    }
}
