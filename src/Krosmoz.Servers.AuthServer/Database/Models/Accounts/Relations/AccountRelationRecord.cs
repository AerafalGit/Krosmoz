// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;

namespace Krosmoz.Servers.AuthServer.Database.Models.Accounts.Relations;

public sealed class AccountRelationRecord
{
    public int Id { get; init; }

    public required int FromAccountId { get; init; }

    public required int ToAccountId { get; init; }

    public required SocialRelationTypeIds RelationType { get; set; }

    public required DateTime CreatedAt { get; init; }
}
