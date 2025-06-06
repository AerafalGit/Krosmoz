// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;

namespace Krosmoz.Protocol.Ipc.Messages.Servers;

public sealed class IpcServerStatusUpdateEvent
{
    public required ushort ServerId { get; init; }

    public required ServerStatuses ServerStatus { get; init; }
}
