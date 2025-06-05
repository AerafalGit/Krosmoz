// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.AuthServer.Database.Models.Accounts.Characters;

public sealed class AccountCharacterRecord
{
    public int Id { get; init; }

    public required ushort ServerId { get; init; }

    public required int AccountId { get; init; }

    public required int CharacterId { get; init; }

    public DateTime? DeletedAt { get; set; }
}
