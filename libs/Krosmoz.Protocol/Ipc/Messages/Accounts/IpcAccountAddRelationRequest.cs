// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;

namespace Krosmoz.Protocol.Ipc.Messages.Accounts;

public sealed class IpcAccountAddRelationRequest
{
    public required int AccountId { get; init; }

    public required int TargetId { get; init; }

    public required SocialRelationTypeIds RelationType { get; init; }
}
