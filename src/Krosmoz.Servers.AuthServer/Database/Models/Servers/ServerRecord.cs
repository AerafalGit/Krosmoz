// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;

namespace Krosmoz.Servers.AuthServer.Database.Models.Servers;

public sealed class ServerRecord
{
    public required ushort Id { get; init; }

    public required string Name { get; set; }

    public required ServerGameTypeIds Type { get; init; }

    public required ServerCommunityIds Community { get; init; }

    public required ServerStatuses Status { get; set; }

    public required GameHierarchies VisibleHierarchy { get; init; }

    public required GameHierarchies JoinableHierarchy { get; init; }

    public IPAddress? IpAddress { get; set; }

    public ushort? Port { get; set; }

    public DateTime? OpenedAt { get; set; }
}
