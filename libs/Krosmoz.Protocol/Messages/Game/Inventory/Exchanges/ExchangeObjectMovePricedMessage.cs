// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public class ExchangeObjectMovePricedMessage : ExchangeObjectMoveMessage
{
	public new const uint StaticProtocolId = 5514;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeObjectMovePricedMessage Empty =>
		new() { Quantity = 0, ObjectUID = 0, Price = 0 };

	public required uint Price { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Price);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Price = reader.ReadVarUInt32();
	}
}
