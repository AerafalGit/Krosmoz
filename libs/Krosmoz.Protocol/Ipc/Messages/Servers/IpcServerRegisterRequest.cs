// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net;

namespace Krosmoz.Protocol.Ipc.Messages.Servers;

public sealed class IpcServerRegisterRequest
{
    public required ushort ServerId { get; init; }

    public required string ServerName { get; init; }

    public required IPAddress ServerAddress { get; init; }

    public required ushort ServerPort { get; init; }
}
