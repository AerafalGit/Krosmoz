// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net;
using System.Net.NetworkInformation;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Ipc.Types.Accounts;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts.Activities;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts.Characters;
using Krosmoz.Servers.AuthServer.Database.Models.Accounts.Relations;

namespace Krosmoz.Servers.AuthServer.Database.Models.Accounts;

public sealed class AccountRecord
{
    public int Id { get; init; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public required GameHierarchies Hierarchy { get; set; }

    public required string SecretQuestion { get; set; }

    public required string SecretAnswer { get; set; }

    public required DateTime CreatedAt { get; init; }

    public required ICollection<AccountCharacterRecord> Characters { get; init; }

    public required ICollection<AccountRelationRecord> Relations { get; init; }

    public AccountActivityRecord? Activity { get; set; }

    public DateTime? SubscriptionStartedAt { get; set; }

    public DateTime? SubscriptionExpiredAt { get; set; }

    public string? Nickname { get; set; }

    public PhysicalAddress? MacAddress { get; set; }

    public IPAddress? IpAddress { get; set; }

    public string? Ticket { get; set; }

    public IpcAccount ToIpcAccount()
    {
        return new IpcAccount
        {
            Id = Id,
            Username = Username,
            Password = Password,
            Hierarchy = Hierarchy,
            SecretQuestion = SecretQuestion,
            SecretAnswer = SecretAnswer,
            CreatedAt = CreatedAt,
            Nickname = Nickname!,
            MacAddress = MacAddress!,
            IpAddress = IpAddress!,
            Ticket = Ticket!,
            Characters = Characters.Select(static x => x.ToIpcAccountCharacter()).ToList(),
            Relations = Relations.Select(static x => x.ToIpcAccountRelation()).ToList(),
            SubscriptionStartedAt = SubscriptionStartedAt,
            SubscriptionExpiredAt = SubscriptionExpiredAt,
            Activity = Activity?.ToIpcAccountActivity()
        };
    }
}
