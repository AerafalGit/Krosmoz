﻿// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Models.Accounts;

public sealed class AccountCharacter
{
    public required int AccountId { get; init; }

    public required int ServerId { get; init; }

    public required long CharacterId { get; init; }
}
