﻿// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net;
using Krosmoz.Protocol.Ipc.Types.Accounts;

namespace Krosmoz.Servers.AuthServer.Database.Models.Accounts.Activities;

public sealed class AccountActivityRecord
{
    public int Id { get; init; }

    public required int AccountId { get; init; }

    public required IPAddress IpAddress { get; set; }

    public required DateTime ConnectedAt { get; set; }

    public IpcAccountActivity ToIpcAccountActivity()
    {
        return new IpcAccountActivity
        {
            AccountId = AccountId,
            IpAddress = IpAddress,
            ConnectedAt = ConnectedAt
        };
    }
}
