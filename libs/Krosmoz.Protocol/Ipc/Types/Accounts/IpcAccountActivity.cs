﻿// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net;

namespace Krosmoz.Protocol.Ipc.Types.Accounts;

public sealed class IpcAccountActivity
{
    public required int AccountId { get; init; }

    public required IPAddress IpAddress { get; set; }

    public required DateTime ConnectedAt { get; set; }
}
