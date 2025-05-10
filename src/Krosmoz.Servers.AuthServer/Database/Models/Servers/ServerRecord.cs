// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;

namespace Krosmoz.Servers.AuthServer.Database.Models.Servers;

public sealed class ServerRecord
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required ServerGameTypeIds Type { get; init; }

    public required ServerStatus Status { get; set; }

    public IPAddress? IpAddress { get; set; }

    public int? Port { get; set; }

    public DateTime? OpenedAt { get; set; }
}
