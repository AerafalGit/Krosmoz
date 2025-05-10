// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net;
using Krosmoz.Protocol.Enums;
using Krosmoz.Servers.AuthServer.Database.Models.Servers;

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

    public required DateTime UpdatedAt { get; set; }

    public required List<ServerCharacterRecord> Characters { get; set; }

    public DateTime? SubscriptionExpireAt { get; set; }

    public string? Nickname { get; set; }

    public IPAddress? IpAddress { get; set; }

    public string? Token { get; set; }
}
