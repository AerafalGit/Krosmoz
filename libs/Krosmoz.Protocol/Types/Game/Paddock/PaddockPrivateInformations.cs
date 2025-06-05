// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Types.Game.Paddock;

public sealed class PaddockPrivateInformations : PaddockAbandonnedInformations
{
	public new const ushort StaticProtocolId = 131;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PaddockPrivateInformations Empty =>
		new() { MaxItems = 0, MaxOutdoorMount = 0, Locked = false, Price = 0, GuildId = 0, GuildInfo = GuildInformations.Empty };

	public required GuildInformations GuildInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		GuildInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		GuildInfo = GuildInformations.Empty;
		GuildInfo.Deserialize(reader);
	}
}
