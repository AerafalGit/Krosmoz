// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses;

public sealed class HouseSoldMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5737;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HouseSoldMessage Empty =>
		new() { HouseId = 0, RealPrice = 0, BuyerName = string.Empty };

	public required int HouseId { get; set; }

	public required int RealPrice { get; set; }

	public required string BuyerName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(HouseId);
		writer.WriteInt(RealPrice);
		writer.WriteUtfLengthPrefixed16(BuyerName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HouseId = reader.ReadInt();
		RealPrice = reader.ReadInt();
		BuyerName = reader.ReadUtfLengthPrefixed16();
	}
}
