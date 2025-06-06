// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;

namespace Krosmoz.Protocol.Ipc.Types.Accounts;

public sealed class IpcAccountRelation
{
    public required int Id { get; init; }

    public required int ToAccountId { get; init; }

    public required SocialRelationTypeIds RelationType { get; set; }
}
