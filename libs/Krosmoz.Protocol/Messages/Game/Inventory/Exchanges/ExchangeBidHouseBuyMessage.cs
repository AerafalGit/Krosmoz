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

	public required uint Uid { get; set; }

	public required uint Qty { get; set; }

	public required uint Price { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Uid);
		writer.WriteVarUInt32(Qty);
		writer.WriteVarUInt32(Price);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Uid = reader.ReadVarUInt32();
		Qty = reader.ReadVarUInt32();
		Price = reader.ReadVarUInt32();
	}
}
