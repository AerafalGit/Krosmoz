// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Ipc.Types.Accounts;

public sealed class IpcAccountCharacter
{
    public required int Id { get; init; }

    public required ushort ServerId { get; init; }

    public required int CharacterId { get; init; }
}
