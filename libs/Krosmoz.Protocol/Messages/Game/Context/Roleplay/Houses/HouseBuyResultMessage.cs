// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses;

public sealed class HouseBuyResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5735;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HouseBuyResultMessage Empty =>
		new() { HouseId = 0, Bought = false, RealPrice = 0 };

	public required int HouseId { get; set; }

	public required bool Bought { get; set; }

	public required int RealPrice { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(HouseId);
		writer.WriteBoolean(Bought);
		writer.WriteInt(RealPrice);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HouseId = reader.ReadInt();
		Bought = reader.ReadBoolean();
		RealPrice = reader.ReadInt();
	}
}
