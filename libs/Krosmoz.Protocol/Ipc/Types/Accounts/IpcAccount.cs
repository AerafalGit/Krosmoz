// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net;
using System.Net.NetworkInformation;
using Krosmoz.Protocol.Enums;

namespace Krosmoz.Protocol.Ipc.Types.Accounts;

public sealed class IpcAccount
{
    public required int Id { get; init; }

    public required string Username { get; init; }

    public required string Password { get; init; }

    public required GameHierarchies Hierarchy { get; init; }

    public required string SecretQuestion { get; init; }

    public required string SecretAnswer { get; init; }

    public required DateTime CreatedAt { get; init; }

    public required string Nickname { get; init; }

    public PhysicalAddress? MacAddress { get; init; }

    public required IPAddress IpAddress { get; init; }

    public required string Ticket { get; init; }

    public required List<IpcAccountCharacter> Characters { get; init; }

    public required List<IpcAccountRelation> Relations { get; init; }

    public IpcAccountActivity? Activity { get; set; }

    public DateTime? SubscriptionStartedAt { get; set; }

    public DateTime? SubscriptionExpiredAt { get; set; }
}
