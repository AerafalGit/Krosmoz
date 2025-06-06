// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net;
using System.Net.NetworkInformation;

namespace Krosmoz.Servers.AuthServer.Database.Models.Banishments;

public sealed class BanishmentRecord
{
    public int Id { get; init; }

    public int? AccountId { get; set; }

    public IPAddress? IpAddress { get; set; }

    public PhysicalAddress? MacAddress { get; set; }

    public string? Reason { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ExpiredAt { get; set; }
}
