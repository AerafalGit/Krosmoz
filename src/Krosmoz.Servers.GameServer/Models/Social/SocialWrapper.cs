// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Friend;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Models.Social;

/// <summary>
/// Represents a wrapper for social interactions, such as friends and ignored players.
/// </summary>
public sealed class SocialWrapper
{
    /// <summary>
    /// Gets or sets the account ID associated with the social wrapper.
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    /// Gets or sets the nickname of the account.
    /// </summary>
    public string AccountNickname { get; set; }

    /// <summary>
    /// Gets or sets the last connection time of the account.
    /// </summary>
    public DateTime LastConnection { get; set; }

    /// <summary>
    /// Gets or sets the character associated with the account, if any.
    /// </summary>
    public CharacterActor? Character { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SocialWrapper"/> class.
    /// </summary>
    /// <param name="accountId">The account ID.</param>
    /// <param name="accountNickname">The account nickname.</param>
    /// <param name="lastConnection">The last connection time.</param>
    /// <param name="character">The character associated with the account, if any.</param>
    public SocialWrapper(int accountId, string accountNickname, DateTime lastConnection, CharacterActor? character = null)
    {
        AccountId = accountId;
        AccountNickname = accountNickname;
        LastConnection = lastConnection;
        Character = character;
    }

    /// <summary>
    /// Retrieves the friend information for the account.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="FriendInformations"/> or <see cref="FriendOnlineInformations"/>
    /// containing the friend details.
    /// </returns>
    public FriendInformations GetFriendInformations()
    {
        return Character is null
            ? new FriendInformations
            {
                AccountId = AccountId,
                AccountName = AccountNickname,
                AchievementPoints = 0, // TODO: Implement achievement points
                LastConnection = (ushort)LastConnection.GetUnixTimestampSeconds(),
                PlayerState = (sbyte)PlayerStates.NotConnected
            }
            : new FriendOnlineInformations
            {
                AccountId = AccountId,
                AccountName = AccountNickname,
                Level = Character.Level,
                Sex = Character.Sex,
                Breed = (sbyte)Character.Breed,
                PlayerId = (uint)Character.Id,
                PlayerName = Character.Name,
                LastConnection = (ushort)LastConnection.GetUnixTimestampSeconds(),
                AchievementPoints = 0, // TODO: Implement achievement points
                PlayerState = (sbyte)Character.State,
                Status = Character.GetPlayerStatus(),
                AlignmentSide = (sbyte)AlignmentSides.AlignmentNeutral, // TODO: Implement alignment side
                MoodSmileyId = 0, // TODO: Implement mood smiley
                GuildInfo = GuildInformations.Empty
            };
    }

    /// <summary>
    /// Retrieves the ignored information for the account.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="IgnoredInformations"/> or <see cref="IgnoredOnlineInformations"/>
    /// containing the ignored details.
    /// </returns>
    public IgnoredInformations GetIgnoredInformations()
    {
        return Character is null
            ? new IgnoredInformations
            {
                AccountId = AccountId,
                AccountName = AccountNickname
            }
            : new IgnoredOnlineInformations
            {
                AccountId = AccountId,
                AccountName = AccountNickname,
                PlayerId = (uint)Character.Id,
                PlayerName = Character.Name,
                Sex = Character.Sex,
                Breed = (sbyte)Character.Breed
            };
    }
}
