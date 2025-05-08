// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Paddock;

public class PaddockBuyableInformations : PaddockInformations
{
	public new const ushort StaticProtocolId = 130;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PaddockBuyableInformations Empty =>
		new() { MaxItems = 0, MaxOutdoorMount = 0, Price = 0, Locked = false };

	public required int Price { get; set; }

	public required bool Locked { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(Price);
		writer.WriteBoolean(Locked);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Price = reader.ReadInt();
		Locked = reader.ReadBoolean();
	}
}
