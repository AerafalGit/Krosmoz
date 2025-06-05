// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Paddock;

public class PaddockAbandonnedInformations : PaddockBuyableInformations
{
	public new const ushort StaticProtocolId = 133;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PaddockAbandonnedInformations Empty =>
		new() { MaxItems = 0, MaxOutdoorMount = 0, Locked = false, Price = 0, GuildId = 0 };

	public required int GuildId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(GuildId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		GuildId = reader.ReadInt32();
	}
}
