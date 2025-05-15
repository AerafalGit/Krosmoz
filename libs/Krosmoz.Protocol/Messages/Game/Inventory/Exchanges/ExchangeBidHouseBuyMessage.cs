// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidHouseBuyMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5804;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidHouseBuyMessage Empty =>
		new() { Uid = 0, Qty = 0, Price = 0 };

	public required int Uid { get; set; }

	public required int Qty { get; set; }

	public required int Price { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(Uid);
		writer.WriteInt32(Qty);
		writer.WriteInt32(Price);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Uid = reader.ReadInt32();
		Qty = reader.ReadInt32();
		Price = reader.ReadInt32();
	}
}
