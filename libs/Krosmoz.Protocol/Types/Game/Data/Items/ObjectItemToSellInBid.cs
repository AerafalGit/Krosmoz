// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public sealed class ObjectItemToSellInBid : ObjectItemToSell
{
	public new const ushort StaticProtocolId = 164;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectItemToSellInBid Empty =>
		new() { ObjectPrice = 0, Quantity = 0, ObjectUID = 0, Effects = [], ObjectGID = 0, UnsoldDelay = 0 };

	public required int UnsoldDelay { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(UnsoldDelay);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		UnsoldDelay = reader.ReadInt32();
	}
}
